using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Dtos.User;

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
        public async Task<IActionResult> UserList(FilterUserDto filter, string fullname, string mobile)
        {
            filter.Fullname = fullname;
            filter.Mobile = mobile;
            var users = await _userService.GetAllUsers(filter);
            return View(users);
        }

        #endregion

        #region Create User

        [HttpGet("create-user")]
        public ActionResult CreateUser()
        {
            return View();
        }


        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userService.CreateUser(user);

            switch (result)
            {
                case CreateUserResult.DuplicateMobile:
                    TempData["DuplicateMobile"] = "شماره همراه تکراری می باشد";
                    return View();
                case CreateUserResult.Success:
                    TempData["Success"] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("UserList", "User", new { area = "administration" });

                case CreateUserResult.Error:
                    return View(user);
                default:
                    return NotFound();
            }

            return View(user);
        }

        #endregion

        #region Edit User

        [HttpGet("edit-user/{id}")]
        public async Task<IActionResult> EditUser(long id)
        {
            var user = await _userService.GetForEditUser(id);

            ViewBag.Fullname = user.Fullname;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost("edit-user/{id}")]
        public async Task<IActionResult> EditUser(EditUserDto editUser)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userService.EditUser(editUser);

            switch (result)
            {
                case EditUserResult.Success:
                    return RedirectToAction("UserList", "User", new { area = "Administration" });
                    break;
                case EditUserResult.NotFoundUser:
                    return NotFound();
                    break;

            }

            return View();
        }

        #endregion

        #region Block and UnBlock User

        [HttpGet("block-user/{id}")]
        public async Task<IActionResult> BlockUser(long id)
        {
            var user = await _userService.BlockUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return RedirectToAction("UserList", "User", new { area = "Administration" });
        }

        [HttpGet("unblock-user/{id}")]
        public async Task<IActionResult> UnBlockUser(long id)
        {
            var user = await _userService.UnBlockUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return RedirectToAction("UserList", "User", new { area = "Administration" });
        }

        #endregion


        #endregion
    }
}
