using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;
using Resume.Domain.IdentityExtensions;

namespace ServiceHost.Areas.Administration.ViewComponents
{
    public class LeftSidebarViewComponent : ViewComponent
    {
        #region Fields

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


            ViewData["User"] = await _userService.GetUserById(User.GetUserId());

            return View("LeftSidebar");
        }
    }
}
