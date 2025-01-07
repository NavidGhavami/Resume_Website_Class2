using MarketPlace.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.User;
using Resume.Application.Services.Interface;
using Resume.Application.Utilities;
using Resume.Domain.Entities.User;
using Resume.Domain.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Resume.Application.Services.Implementation
{
	public class UserService : IUserService
	{
		private readonly IGenericRepository<User> _userRepository;
		private readonly IPasswordHasher _passwordHasher;

		#region Constructor

		public UserService(IGenericRepository<User> userRepository, IPasswordHasher passwordHasher)
		{
			_userRepository = userRepository;
			_passwordHasher = passwordHasher;
		}

		#endregion

		#region Methods

		public async Task<UserDetailDto> GetUserInformation()
		{
			var user = await _userRepository.GetQuery().AsQueryable().FirstOrDefaultAsync(x => !x.IsDelete);

			if (user == null)
			{
				return new UserDetailDto();
			}

			return new UserDetailDto
			{
				Fullname = user.Fullname,
				Email = user.Email,
				Mobile = user.Mobile,
				BirthDate = user.BirthDate,
				BirthPlace = user.BirthPlace,
				Description = user.Description,
				Avatar = user.Avatar
			};
		}
		public async Task<bool> IsUserExistByMobile(string mobile)
		{
			return await _userRepository.GetQuery().AnyAsync(x => x.Mobile == mobile);
		}
		public async Task<List<FilterUserDto>> FilterUsers(FilterUserDto filter)
		{
			var query = _userRepository
				.GetQuery()
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.Fullname))
			{
				query = query.Where(x => EF.Functions.Like(x.Fullname, $"%{filter.Fullname}%"));
			}

			if (!string.IsNullOrWhiteSpace(filter.Mobile))
			{
				query = query.Where(x => EF.Functions.Like(x.Mobile, $"%{filter.Mobile}%"));
			}

			var allUser = query.Select(x => new FilterUserDto
			{
				Id = x.Id,
				Fullname = x.Fullname,
				Mobile = x.Mobile,
				Email = x.Email,
				IsBlock = x.IsBlock,
				Avatar = x.Avatar,
				CreateDate = x.CreateDate.ToStringShamsiDate()
			});


			return await allUser.OrderByDescending(x => x.Id).ToListAsync();
		}
		public async Task<CreateUserResult> CreateUser(CreateUserDto user)
		{
			if (!await IsUserExistByMobile(user.Mobile))
			{
				var hashPassword = _passwordHasher.EncodePasswordMd5(user.Password);
				var newUser = new User
				{
					Fullname = user.Fullname,
					Email = user.Email,
					Mobile = user.Mobile,
					BirthDate = user.BirthDate,
					BirthPlace = user.BirthPlace,
					Description = user.Description,
					Password = hashPassword,
					ConfirmPassword = hashPassword,
					IsBlock = false,
					Avatar = null
				};

				await _userRepository.AddEntity(newUser);
				await _userRepository.SaveChanges();

				return CreateUserResult.Success;
			}
			else
			{
				return CreateUserResult.MobileExist;
			}
		}
		public async Task<EditUserDto> GetUserForEdit(long id)
		{
			var user = await _userRepository.GetQuery().AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

			if (user.Id == 0)
			{
				return new EditUserDto();
			}

			return new EditUserDto
			{
				Id = user.Id,
				Fullname = user.Fullname,
				Email = user.Email,
				Mobile = user.Mobile,
				BirthDate = user.BirthDate,
				BirthPlace = user.BirthPlace,
				Description = user.Description,
				Password = user.Password,
				ConfirmPassword = user.ConfirmPassword,
				PasswordSalt = user.PasswordSalt,
				IsBlock = user.IsBlock,
				Avatar = user.Avatar,
			};
		}
		public async Task<EditUserResult> EditUser(EditUserDto user)
		{
			var mainUser = await _userRepository.GetQuery().AsQueryable().FirstOrDefaultAsync(x => x.Id == user.Id);
			//var hashPassword = _passwordHasher.EncodePasswordMd5(user.Password);
			//var hashPassword = PasswordHasher.HashPasswordSHA256(user.Password);

			var salt = NewHashPassword.GenerateSalt();
			var hashPassword = NewHashPassword.HashPassword(user.Password, salt);

			if (user.Id == 0)
			{
				return EditUserResult.NotFound;
			}

			mainUser.Fullname = user.Fullname;
			mainUser.Email = user.Email;
			mainUser.Mobile = user.Mobile;
			mainUser.BirthDate = user.BirthDate;
			mainUser.BirthPlace = user.BirthPlace;
			mainUser.Description = user.Description;
			mainUser.Password = hashPassword;
			mainUser.ConfirmPassword = hashPassword;
			mainUser.PasswordSalt = salt;
			mainUser.Avatar = user.Avatar;
			mainUser.IsBlock = user.IsBlock;

			_userRepository.EditEntity(mainUser);
			await _userRepository.SaveChanges();


			return EditUserResult.Success;
		}
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
		public async Task<bool> UnblockUser(long id)
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
		public async Task<UserLoginResult> UserLogin(UserLoginDto login)
		{
			//var hashPassword = _passwordHasher.EncodePasswordMd5(login.Password);

            var storedSalt = await GetStoredSalt(login.Mobile);
            var storedHashPassword = await GetStoredPassword(login.Mobile);
            var hashPassword = NewHashPassword.HashPassword(login.Password, storedSalt);
            var isPasswordValid = NewHashPassword.CompareHashes(hashPassword, storedHashPassword);

			var user = await _userRepository
				.GetQuery()
				.AsQueryable()
				.FirstOrDefaultAsync(x => x.Mobile == login.Mobile && x.Password == hashPassword);


			if (user == null)
			{
				return UserLoginResult.Error;
			}



            return isPasswordValid ? UserLoginResult.Success : UserLoginResult.UserNotFound;

        }
		public async Task<User> GetUserBy(string mobile)
		{
			var user = await _userRepository
				.GetQuery()
				.AsQueryable()
				.FirstOrDefaultAsync(x => x.Mobile == mobile);

            if (user == null)
            {
                return null;
            }

			return new User
			{
				Id = user.Id,
				Fullname = user.Fullname,
				Mobile = user.Mobile,
				Password = user.Password,
				Avatar = user.Avatar
			};
		}
		public async Task<UserDetailDto> GetUserById(long id)
		{
			var user = await _userRepository.GetEntityById(id);

			if (user == null)
			{
				return null;
			}

			return new UserDetailDto
			{
				Id = user.Id,
				Fullname = user.Fullname,
				Mobile = user.Mobile,
				Email = user.Email,
				Avatar = user.Avatar,
				IsBlock = user.IsBlock
			};
		}

        public async Task<string> GetStoredSalt(string mobile)
        {
            var user  = await _userRepository.GetQuery().AsQueryable().FirstOrDefaultAsync(x => x.Mobile == mobile);

            return user.PasswordSalt;
        }

        public async Task<string> GetStoredPassword(string mobile)
        {
            var user = await _userRepository.GetQuery().AsQueryable().FirstOrDefaultAsync(x => x.Mobile == mobile);

            return user.Password;
        }

        #endregion

		#region Dispose

		public async ValueTask DisposeAsync()
		{
			if (_userRepository != null)
			{
				_userRepository.DisposeAsync();
			}
		}

		#endregion
	}
}
