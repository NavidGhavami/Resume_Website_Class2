using MarketPlace.Application.Services.Implementations;
using MarketPlace.Application.Services.Interfaces;
using Resume.Application.Services.Implementation;
using Resume.Application.Services.Interface;
using Resume.Domain.Repository;

namespace ServiceHost.Configuration
{
    public static class DiContainer
    {
        public static void RegisterService(this IServiceCollection services)
        {
            #region Repositories

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPasswordHasher, PasswordHasher>();


            #endregion


            #region General Services

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEducationService, EducationService>();
            services.AddTransient<IExperienceService, ExperienceService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IBlogService, BlogService>();

            #endregion
        }
    }
}
