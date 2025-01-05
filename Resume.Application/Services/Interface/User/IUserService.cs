using Resume.Domain.Dtos.User;

namespace Resume.Application.Services.Interface.User
{
	public interface IUserService : IAsyncDisposable
    {
        Task<bool> IsUserExistByMobile(string mobile);
        Task<List<FilterUserDto>> GetAllUsers(FilterUserDto filter); 
        Task<UserDetailDto> GetUserDetail();
        Task<CreateUserResult> CreateUser(CreateUserDto user);
        Task<EditUserDto> GetForEditUser(long id);
        Task<EditUserResult> EditUser(EditUserDto command);
        Task<bool> BlockUser(long id);
        Task<bool> UnBlockUser(long id);
        Task<UserLoginResult> UserLogin(LoginDto login);
        Task<Domain.Entities.User.User> GetUserByMobile(string mobile);
        Task<UserDetailDto> GetUserById(long id);


    }
}
