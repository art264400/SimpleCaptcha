# SimpleCaptcha

<img src="https://lh3.googleusercontent.com/fife/ABSRlIqdgattEFvcXP1k5RmJ-oZDpMG40p3445GsEceMC2DwsZHUzHKW_U0qBzo7mu19ZfdMT38GFCT22r2PMnrWQvq0mcS7dbWAx9NNLJz0PgQ-Y5Cv8jUCClXQUTagbQy_qd2ase6ICfJH6hf-PyiDEMj_W_HfnWe5yBTGCMdGlvNHZSXi-Qr5OESRRvFxLhTbXkbjJJXInKhDZULm_d9FnaFr74BjToga1sHBdpRQvuzNSmZfu3XtIj5K0X4QIwPQJpSyM_8TbrodrVmKHEtEuSRf7NQlPUZmM8nk8ejWF4WsEjyEL4FiH4n6dTe2c2pRo6T_NHUp7rLsV33XYRmlQs1l5jJeWR8N6b9aO8iDazTL-nEisU_FVv44GCM298GO_LQKi4yqb_kVhkdu3c8b-qlD0gN3DnU46PZWz-NHTkBqvRmKuYD55jSpOxPsRQNECG_zL_Ay_o6ysVixzJXPSziDt8-3TBurW_364Po5q_VoImBzDTfgiIXSwYuUw56f8N81Zd7Ou3fURcU6skd4QKEonDE6YQIlqhUFxLMjgx-SU6TNoe4mxp4v6-CtxvWBQnb_wustJLrdYp4T75fj0GIW44106wtejfRzluzFnxJ8t43u2PZ3oFURdljtnqygDkVTAUlBcyPnlG0VFLtNPlO0BShyf0yUZOPtTwyFvoUZ0Occ3PjR4ueCqLZu2FGgo8FQgY1ezqHQlTVtV0f0NT05ZSlPOdHqz2k=w1843-h977-ft" />
<h1>Step 1</h1>
<h6>Installation</h6>

``` Install-package SimpleCaptchaNetCore ```


<h1>Step 2</h2>
<h6>Add  lines into your app's Startup class (Startup.cs):</h6>

```csharp
  public void Configure(IApplicationBuilder app, IHostingEnvironment env)
  {
    ...
    // configure Session middleware
    app.UseSession();
    // configure your application pipeline to use Captcha middleware
    // Important! UseCaptcha(...) must be called after the UseSession() call
    app.UseCaptcha(Configuration);
    ...
  }
```

```csharp
  public void ConfigureServices(IServiceCollection services)
   {
     ...
     services.Configure<KestrelServerOptions>(options =>
      {
        options.AllowSynchronousIO = true;
      });
     services.Configure<IISServerOptions>(options =>
      {
        options.AllowSynchronousIO = true;
      });
     ...
   }
```
<h1>Step 3</h2>
<h6>Make the CaptchaTagHelper class available to all Razor views in your app, by adding the addTagHelper directive to the Views/_ViewImports.cshtml file:</h6>

```csharp
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper SimpleCaptcha.Helpers.CaptchaTagHelper, SimpleCaptcha
```

<h6>To the *.chtml file at the exact place where you want to be displaying captcha  add the <captcha> tag.</h6>
	
```csharp 
<captcha reload-icon-url="@Url.Content($"~/img/update.png")"/>
 ```

<h1>Step 4 </h2>
<h6>Add a check:</h6>

```csharp
SimpleCaptcha.Validator.Validate(userInput,HttpContext) //Returns true or false
```

<h1>Step 5 OPTIONAL</h2>
<h6>appsetting.json</h6>

```csharp
 "SimpleCaptcha": {
    "Height": 35,
    "Width": 120,
    "Locale": "en",
    "CapthcaLenght": 6 
  }
```
	
