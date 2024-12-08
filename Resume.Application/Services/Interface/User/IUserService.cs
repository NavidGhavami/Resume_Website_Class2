using Resume.Domain.Dtos.User;

namespace Resume.Application.Services.Interface.User
{
	public interface IUserService : IAsyncDisposable
    {
        Task<bool> IsUserExistByMobile(string mobile);
        Task<List<FilterUserDto>> GetAllUsers(FilterUserDto filter); 
        Task<CreateUserResult> CreateUser(CreateUserDto user);
        Task<EditUserDto> GetForEditUser(long id);
        Task<EditUserResult> EditUser(EditUserDto command);
        Task<bool> BlockUser(long id);
        Task<bool> UnBlockUser(long id);


    }
}
