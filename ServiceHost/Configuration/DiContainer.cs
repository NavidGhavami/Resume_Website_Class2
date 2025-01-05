using Resume.Application.Services.Implementation;
using Resume.Application.Services.Implementation.User;
using Resume.Application.Services.Interface;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Repository;

namespace ServiceHost.Configuration
{
    public static class DiContainer
    {
        public static void RegisterService(this IServiceCollection services)
        {
            #region Repositories

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            #endregion

            #region General Services

            services.AddTransient<IUserService, UserService>();

            #endregion
        }
    }
}
