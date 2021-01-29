using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SimpleCaptcha
{
    public class CaptchaMiddleware
    {
        RequestDelegate _next;
        IConfiguration _configuration;
        public CaptchaMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _configuration = configuration;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Query.ContainsKey("ImageCaptcha"))  /* && !string.IsNullOrEmpty(context.Request.Query.ContainsKey("CaptchaGuid").ToString()))*/
            {
                using (var fileStream = new MemoryStream())
                {
                    try
                    {
                        var Height = Convert.ToInt32(_configuration["SimpleCaptcha:Height"]);
                        var Width = Convert.ToInt32(_configuration["SimpleCaptcha:Width"]);
                        var Locale = _configuration["SimpleCaptcha:Locale"];
                        var CaptchaLenght= Convert.ToInt32(_configuration["SimpleCaptcha:CapthcaLenght"]);
                        var simpleCaptcha = new SimpleCaptcha(Height, Width, Locale, CaptchaLenght).Generate(fileStream);
                        context.Session.SetString("code", simpleCaptcha);
                        context.Response.ContentType = "image/jpeg";
                        fileStream.WriteTo(context.Response.Body);
                    }
                    catch(Exception ex)
                    {
                        var simpleCaptcha = new SimpleCaptcha().Generate(fileStream);
                        context.Session.SetString("code", simpleCaptcha);
                        context.Response.ContentType = "image/jpeg";
                        fileStream.WriteTo(context.Response.Body);
                        await _next(context);
                    }
                }
            }
            await _next(context);
        }
    }
    public static class CaptchaExtensions
    {
        public static IApplicationBuilder UseCaptcha(this IApplicationBuilder builder, IConfiguration configuration)
        {
            return builder.UseMiddleware<CaptchaMiddleware>(configuration);
        }
    }
}
