using MarketPlace.Application.Utilities;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface;
using Resume.Application.Services.Interface.User;
using Resume.Application.Utilities;
using Resume.Domain.Dtos.User;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.User
{
    public class UserService : IUserService
    {
        #region fields

        private readonly IGenericRepository<Domain.Entities.User.User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        #endregion


        #region Constructor

        public UserService(IGenericRepository<Domain.Entities.User.User> userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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
                var hashPassword = _passwordHasher.EncodePasswordMd5(user.Password);
                var newUser = new Domain.Entities.User.User
                {
                    Fullname = user.Fullname,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Password = hashPassword,
                    ConfirmPassword = hashPassword,
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

        #region Edit User

        public async Task<EditUserDto> GetForEditUser(long id)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x=>x.Id == id);


            if (user == null)
            {
                return new EditUserDto();
            }

            return new EditUserDto
            {
                Id = id,
                Fullname = user.Fullname,
                Mobile = user.Mobile,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword,
                IsBlock = user.IsBlock,
                Avatar = user.Avatar
            };
        }

        public async Task<EditUserResult> EditUser(EditUserDto command)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == command.Id);

            if (user == null)
            {
                return EditUserResult.NotFoundUser;
            }

            //var hashPassword = _passwordHasher.EncodePasswordMd5(user.Password);
            var hashPassword = SHA256PasswordHasher.HashPasswordSHA256(command.Password);

            user.Fullname = command.Fullname;
            user.Email = command.Email;
            user.Password = hashPassword;
            user.ConfirmPassword = hashPassword;
            user.Mobile = command.Mobile;
            user.IsBlock = command.IsBlock;
            user.Avatar = command.Avatar;
            user.UpdateDate = DateTime.Now;

            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();

            return EditUserResult.Success;

        }

        #endregion

        #region Block and UnBlock User

        public async Task<bool> BlockUser(long id)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            user.IsBlock = true;

            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();

            return true;
        }

        public async Task<bool> UnBlockUser(long id)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            user.IsBlock = false;

            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();

            return true;
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
