using Microsoft.Extensions.Logging;
using AutoMapper;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Utility;
using Wata.Commerce.Account.Domain.Models;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Repositories;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Business.Services
{
    public class UserService : IUserService
    {
		#region Fields
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		protected readonly IUserRepository _userRepository;
		#endregion

		#region Constructors
		public UserService(
			ILogger<UserService> logger,
			IMapper mapper,
			IUserRepository userRepository)
        {
			_logger = logger;
			_mapper = mapper;
            _userRepository = userRepository;
        }
		#endregion

		#region Insert User
		public async Task<UserDto?> InsertUserAsync(UserRequestDto dto)
        {
			User user = new User();
			user.Id = dto.Id;
			user.AccessFailedCount = dto.AccessFailedCount;
			user.ConcurrencyStamp = dto.ConcurrencyStamp;
			user.Email = dto.Email;
			user.EmailConfirmed = dto.EmailConfirmed;
			user.LockoutEnabled = dto.LockoutEnabled;
			user.LockoutEnd = dto.LockoutEnd;
			user.NormalizedEmail = dto.NormalizedEmail;
			user.NormalizedUserName = dto.NormalizedUserName;
			user.PasswordHash = dto.PasswordHash;
			user.PhoneNumber = dto.PhoneNumber;
			user.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
			user.SecurityStamp = dto.SecurityStamp;
			user.TwoFactorEnabled = dto.TwoFactorEnabled;
			user.UserName = dto.UserName;

			User? newUser = await _userRepository.InsertAsync(user);

			if (newUser != null)
			{
				return _mapper.Map<User, UserDto>(newUser);
			}

			return null;
        }
		#endregion

		#region Update User
		public async Task<int> UpdateUserAsync(UserRequestDto dto)
        {

			User? user = await _userRepository.GetByIdAsync(dto.Id);
			if(user != null)
			{
				user.AccessFailedCount = dto.AccessFailedCount;
				user.ConcurrencyStamp = dto.ConcurrencyStamp;
				user.Email = dto.Email;
				user.EmailConfirmed = dto.EmailConfirmed;
				user.LockoutEnabled = dto.LockoutEnabled;
				user.LockoutEnd = dto.LockoutEnd;
				user.NormalizedEmail = dto.NormalizedEmail;
				user.NormalizedUserName = dto.NormalizedUserName;
				user.PasswordHash = dto.PasswordHash;
				user.PhoneNumber = dto.PhoneNumber;
				user.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
				user.SecurityStamp = dto.SecurityStamp;
				user.TwoFactorEnabled = dto.TwoFactorEnabled;
				user.UserName = dto.UserName;

				return await _userRepository.UpdateAsync(user);
			}

            return 0;
        }
		#endregion

		#region Delete User
		public async Task<int> DeleteUserAsync(string id)
        {

			return await _userRepository.DeleteAsync(id);
        }
		#endregion

		#region Get User
		public async Task<UserDto?> GetUserAsync(string id, bool isDeep = false)
        {

			User? user = await _userRepository.GetByIdAsync(id, isDeep);
			if(user != null)
			{
				return _mapper.Map<User, UserDto>(user);
			}

            return null;
        }
		#endregion

		#region Get List Users
		public async Task<PagedDto<UserDto>> GetListUsersAsync(UserFilterDto filterDto)
        {
			PagedDto<User> dt = await _userRepository.GetListAsync(_mapper.Map<UserFilterDto, UserFilter>(filterDto));

			List<UserDto> dtos = new List<UserDto>();
			foreach(User item in dt.Data)
            {
				dtos.Add(_mapper.Map<User, UserDto>(item));
            }

			return new PagedDto<UserDto>(dt.TotalRecords, dtos);
        }
		#endregion
	}
}