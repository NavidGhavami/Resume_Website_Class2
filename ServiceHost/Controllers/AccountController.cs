using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Dtos.User;

namespace ServiceHost.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Fields

        private readonly IUserService _userService;


        #endregion

        #region Constructor

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion


        #region Actions


        #region Login

        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Administration" });
            }

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto command)
        {

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await _userService.UserLogin(command);

            switch (result)
            {
                case UserLoginResult.Success:
                    var user = await _userService.GetUserByMobile(command.Mobile);

                    #region Login

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, $"{user.Fullname}"),
                        new Claim(ClaimTypes.MobilePhone, user.Mobile)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(principal, properties);

                    #endregion

                    TempData[SuccessMessage] = $"{user.Fullname} عزیز، خوش آمدید";

                    return RedirectToAction("Index", "Home", new { area = "Administration" });

                case UserLoginResult.Error:
                    TempData[ErrorMessage] = "در انجام عملیات خطایی رخ داد. لطفا دوباره تلاش نمایید";
                    break;
                case UserLoginResult.NotUserFound:
                    TempData[ErrorMessage] = "کاربری با مشخصات فوق یافت نشد";
                    break;

            }

            return View();
        }

        #endregion


        #region Logout

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion



        #endregion
    }
}
