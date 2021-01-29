using Microsoft.AspNetCore.Http;

namespace SimpleCaptcha
{
    public static class Validator
    {
        public static bool Validate(string userInput, HttpContext context)
        {
            if (context.Session.GetString("code") == userInput)
                return true;
            return false;
        }
    }
}
