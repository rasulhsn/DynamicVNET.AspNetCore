## DynamicVNET For Asp.Net Core - Overview [BETA]
[![NuGet](https://img.shields.io/badge/nuget-1.0.1_alpha-blue.svg)](https://www.nuget.org/packages/DynamicVNET.Lib.AspNetCore/1.0.1-alpha)

### Support
 - Only Profile Validation.
 - Populate Automatic ModelState Errors.

### Asp.Net Core Example
Validation Model
```csharp
// Simulation
 public class ViewModel
 {
        public string Id { get; set; }
 }
```
Step : Startup.ConfigureServices
```csharp
public void ConfigureServices(IServiceCollection services)
{
            services.AddProfileValidation(validator =>
            {
                validator.AddProfile<ViewModel>("StrProfile", marker =>
                {
                    marker
                    .Required(x => x.Id)
                       .StringLen(x => x.Id, 3);
                });

                validator.AddProfile<ViewModel>("PhoneProfile", marker =>
                {
                    marker
                        .Required(x => x.Id)
                        .PhoneNumber(x => x.Id)
                        .Range(x => x.Id, 7, 12);
                });
            });

            // other .......
}
```
Step : Any Controller Action Implementation
```csharp
public class HomeController : Controller
{
        public IActionResult Index()
        {
            return View();
        }

        [ProfileVNET]
        public IActionResult Contact([ProfileBind("PhoneProfile")]ViewModel model,[ProfileBind("StrProfile")]ViewModel model2)
        {
            if(!ModelState.IsValid)
                ViewData["Message"] = "Error Model";
            else
                ViewData["Message"] = "Success Model";

            return View();
        }
}
```

### Where can I get it?

Install [DynamicVNET.AspNetCore](https://www.nuget.org/packages/DynamicVNET.Lib.AspNetCore/) from the package manager console:

```
PM> Install-Package DynamicVNET.Lib.AspNetCore -Version 1.0.1-alpha
```

### License & Copyright

[DynamicVNET.AspNetCore](https://github.com/rasulhsn/DynamicVNET.AspNetCore) is Copyright © 2018 Rasul Huseynov and lincensed under the [MIT license](https://github.com/rasulhsn/DynamicVNET.AspNetCore/blob/master/LICENSE).
