using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.User;
using Resume.Application.Services.Interface;

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

        #region User List


        [HttpGet("users-list")]
        public async Task<IActionResult> UserList(FilterUserDto filter)
        {
            var users = await _userService.FilterUsers(filter);
            return View(users);
        }


        #endregion

        #region Create User

        [HttpGet("create-user")]
        public IActionResult CreateUser()
        {
            return View();
        }


        [HttpPost("create-user"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await _userService.CreateUser(user);

            switch (result)
            {
                case CreateUserResult.Success:
                    await _userService.CreateUser(user);
                    return RedirectToAction("UserList", "User", new { area = "Administration" });

                case CreateUserResult.MobileExist:
                    TempData["ErrorMessage"] = ErrorMessage;
                    break;
                case CreateUserResult.Error:
                    TempData["ErrorMessage"] = ErrorMessage;
                    break;
            }

            return View(user);
        }

        #endregion

        #region Edit User

        [HttpGet("edit-user/{id}")]
        public async Task<IActionResult> EditUser(long id)
        {
            var user = await _userService.GetUserForEdit(id);


            ViewBag.FullName = user.Fullname;

            if (user.Id == 0)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            return View(user);
        }


        [HttpPost("edit-user/{id}")]
        public async Task<IActionResult> EditUser(EditUserDto editUser)
        {
            if (!ModelState.IsValid)
            {
                return View(editUser);
            }

            var result = await _userService.EditUser(editUser);

            switch (result)
            {
                case EditUserResult.NotFound:
                    return RedirectToAction("PageNotFound", "Home");
                case EditUserResult.Success:
                    return RedirectToAction("UserList", "User", new { area = "Administration" });
            }

            return View();
        }

        #endregion

        #region Block and Unblock User

        [HttpGet("block-user/{id}")]
        public async Task<IActionResult> BlockUser(long id)
        {
            var user = await _userService.BlockUser(id);

            return user == null ? RedirectToAction("PageNotFound", "Home") : RedirectToAction("UserList", "User", new { area = "Administration" });
        }

        [HttpGet("unblock-user/{id}")]
        public async Task<IActionResult> UnblockUser(long id)
        {
            var user = await _userService.UnblockUser(id);

            return user == null ? RedirectToAction("PageNotFound", "Home") : RedirectToAction("UserList", "User", new { area = "Administration" });
        }

        #endregion


        #endregion
    }
}
