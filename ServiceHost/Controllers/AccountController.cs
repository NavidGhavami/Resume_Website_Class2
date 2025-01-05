using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.User;
using Resume.Application.Services.Interface;

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
		public async Task<IActionResult> Login(UserLoginDto userLogin)
		{
			if (!ModelState.IsValid)
			{
				return View(userLogin);
			}

			var result = await _userService.UserLogin(userLogin);

			switch (result)
			{
				case UserLoginResult.Success:
					var user = await _userService.GetUserBy(userLogin.Mobile);

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

					TempData[SuccessMessage] = $"{user.Fullname} عزیز، خوش آمدید";

					#endregion

					return RedirectToAction("Index", "Home", new { area = "Administration" });

                case UserLoginResult.Error:
					TempData[ErrorMessage] = "خطایی رخ داد. لطفا مجددا تلاش نمایید";
                    return View(userLogin);
				case UserLoginResult.UserNotFound:
					TempData[ErrorMessage] = "کاربری با مشخصات فوق یافت نشد";
                    return View(userLogin);
			}

			return View();
		}

		#endregion

		#region Logout

		[HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
			return RedirectToAction("Index","Home");
        }

        #endregion

		#endregion
	}
}
