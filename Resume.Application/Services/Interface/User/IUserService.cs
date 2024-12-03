using Resume.Domain.Dtos.User;

namespace Resume.Application.Services.Interface.User
{
	public interface IUserService : IAsyncDisposable
    {
        Task<bool> IsUserExistByMobile(string mobile);
        Task<List<FilterUserDto>> GetAllUsers(FilterUserDto filter);
        public Task<CreateUserResult> CreateUser(CreateUserDto user);
    }
}
