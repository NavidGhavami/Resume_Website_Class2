using MarketPlace.Application.Utilities;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Dtos.User;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.User
{
    public class UserService : IUserService
    {
        #region fields

        private readonly IGenericRepository<Domain.Entities.User.User> _userRepository;



        #endregion


        #region Constructor

        public UserService(IGenericRepository<Domain.Entities.User.User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Duplicate User

        public async Task<bool> IsUserExistByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().AnyAsync(x => x.Mobile == mobile);
        }

        #endregion

        #region Filter Users

        public async Task<List<FilterUserDto>> GetAllUsers(FilterUserDto filter)
        {
            var query = _userRepository.GetQuery().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Fullname))
            {
                query = query.Where(x => EF.Functions.Like(x.Fullname, $"%{filter.Fullname}%"));
                //query = query.Where(x => x.Fullname.Contains(filter.Fullname));
            }

            if (!string.IsNullOrWhiteSpace(filter.Mobile))
            {
                query = query.Where(x => EF.Functions.Like(x.Mobile, $"%{filter.Mobile}%"));
            }

            var allUsers = query.Select(x => new FilterUserDto
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Mobile = x.Mobile,
                Email = x.Email,
                IsBlock = x.IsBlock,
                Avatar = x.Avatar,
                CreateDate = x.CreateDate.ToStringShamsiDate()
            }).ToListAsync();


            return await allUsers;
        }
        
        #endregion


        #region Create User



        public async Task<CreateUserResult> CreateUser(CreateUserDto user)
        {


            if (!await IsUserExistByMobile(user.Mobile))
            {
                var newUser = new Domain.Entities.User.User
                {
                    Fullname = user.Fullname,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Password = user.Password,
                    ConfirmPassword = user.ConfirmPassword,
                    IsBlock = user.IsBlock,
                    Avatar = null,
                };

                await _userRepository.AddEntity(newUser);
                await _userRepository.SaveChanges();

                return CreateUserResult.Success;
            }

            return CreateUserResult.DuplicateMobile;


        }


        #endregion





        #region Dispose

        public async ValueTask DisposeAsync()
	    {
            if ( _userRepository != null)
            {
                await _userRepository.DisposeAsync();
            }
	    }

        
        #endregion
    }
}
