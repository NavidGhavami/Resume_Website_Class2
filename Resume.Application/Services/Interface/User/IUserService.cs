using Resume.Domain.Dtos.User;

namespace Resume.Application.Services.Interface.User
{
	public interface IUserService : IAsyncDisposable
    {
        Task<bool> IsUserExistByMobile(string mobile);
        Task<FilterUserDto> GetAllUsers();
        public Task<CreateUserResult> CreateUser(CreateUserDto user);
    }
}
