using Applications.Models;
using Infrastructure;
using Infrastructure.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WebAPP.Helper;
using WebAPP.Models;
using WebAPP.Models.Static;

namespace WebAPP.Controllers
{
    public class AccountController : Controller
    {
        private string _BaseURL;
        public AccountController(AppSetting appSetting)
        {
            _BaseURL = appSetting.WebAPIBaseUrl;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View(new LoginVM { });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var res = new LoginVM { Message = "Invalid login request!" };
            if (ModelState.IsValid)
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseURL}/api/account/login", JsonConvert.SerializeObject(loginRequest));
                if (!string.IsNullOrEmpty(apiRes.Result))
                {
                    var authenticateResponse = JsonConvert.DeserializeObject<Response<LoginResponse>>(apiRes.Result);
                    if (authenticateResponse.StatusCode != ResponseStatus.Success)
                    {
                        res.Message = authenticateResponse.ResponseText;
                        return View(res);
                    }
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim("Token", authenticateResponse.Result.Token));
                    identity.AddClaim(new Claim(ClaimTypes.Role, authenticateResponse.Result.Role));
                    identity.AddClaim(new Claim(ClaimTypes.Name, authenticateResponse.Result.Name));
                    identity.AddClaim(new Claim("UserId", authenticateResponse.Result.UserId.ToString()));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    string redirectUrl = $"/Dashboard/{authenticateResponse.Result.Role}";
                    return LocalRedirect(redirectUrl);
                }
            }
            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = "/Account/Login")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return LocalRedirect(returnUrl);
        }
    }
}
