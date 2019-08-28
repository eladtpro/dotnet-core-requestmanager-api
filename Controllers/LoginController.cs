using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using User = RequestManager.Model.User;

namespace RequestManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger logger;

        public LoginController(ILogger<LoginController> logger) { this.logger = logger; }

        //[HttpGet]
        //public User Get()
        //{
        //    return this.Identity;
        //}

        //[HttpGet]
        //public IActionResult SignIn()
        //{
        //    string redirectUrl = Url.Action(nameof(RequestController), "Home");
        //    return Challenge(
        //        new AuthenticationProperties { RedirectUri = redirectUrl },
        //        OpenIdConnectDefaults.AuthenticationScheme);
        //}

        //[HttpGet]
        //public IActionResult SignOut()
        //{
        //    string callbackUrl = Url.Action(nameof(SignOut), "Account", values: null, protocol: Request.Scheme);
        //    return SignOut(
        //        new AuthenticationProperties { RedirectUri = callbackUrl },
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        OpenIdConnectDefaults.AuthenticationScheme);
        //}
    }
}