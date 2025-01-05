using Microsoft.AspNetCore.Mvc;
using Resume.Application.IdentityExtensions;
using Resume.Application.Services.Interface;

namespace ServiceHost.Areas.Administration.ViewComponents
{
	#region Left Sidebar

	public class LeftSidebarViewComponent : ViewComponent
	{
		#region Fieilds

		private readonly IUserService _userService;

		#endregion


		#region Constructor

		public LeftSidebarViewComponent(IUserService userService)
		{
			_userService = userService;
		}

		#endregion

		public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserById(User.GetUserId());

            ViewData["User"] = await _userService.GetUserById(User.GetUserId());
			return View("LeftSidebar");
		}
	}

	#endregion
}
