using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class UserController : AdminBaseController
    {
        #region Fields

        private readonly IUserService _userService;



        #endregion

        #region Constructor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions

        #region Users List

        [HttpGet("users-list")]
        public async Task<IActionResult> UserList()
        {
            return View();
        }

        #endregion

        #region Create User



        #endregion

        #region Edit User

        

        #endregion


        #endregion
    }
}
