﻿using Microsoft.AspNetCore.Http;
using Resume.Application.Dtos.User;
using Resume.Domain.Entities.User;

namespace Resume.Application.Services.Interface;

public interface IUserService : IAsyncDisposable
{
	Task<UserDetailDto> GetUserInformation();
    Task<bool> IsUserExistByMobile(string mobile);
    Task<List<FilterUserDto>> FilterUsers(FilterUserDto filter);
    Task<CreateUserResult> CreateUser(CreateUserDto user);
    Task<EditUserDto> GetUserForEdit(long id);
    Task<EditUserResult> EditUser(EditUserDto user, IFormFile avatarImage);
    Task<bool> BlockUser(long id);
    Task<bool> UnblockUser(long id);
    Task<UserLoginResult> UserLogin(UserLoginDto login);
    Task<User> GetUserBy(string mobile);
    Task<UserDetailDto> GetUserById(long id);
    Task<string> GetStoredSalt(string mobile);
    Task<string> GetStoredPassword(string mobile);
}