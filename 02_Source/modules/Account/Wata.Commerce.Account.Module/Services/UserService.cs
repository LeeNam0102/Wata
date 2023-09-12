using Microsoft.Extensions.Logging;
using AutoMapper;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Client.Services;
using Wata.Commerce.Account.Dtos;
using Wata.Commerce.Account.Module.Models.User;

namespace Wata.Commerce.Account.Module.Services
{
    public class UserService : IUserService
    {
		#region Fields
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		protected readonly IUserClient _userClient;
		#endregion

		#region Constructors
		public UserService(
			ILogger<UserService> logger,
			IMapper mapper,
			IUserClient userClient)
        {
			_logger = logger;
			_mapper = mapper;
            _userClient = userClient;
        }
		#endregion

		#region Insert User
		public async Task<UserModel?> InsertUserAsync(UserModel model)
        {
			UserDto? newUser = await _userClient.InsertAsync(_mapper.Map<UserModel, UserRequestDto>(model));

            if (newUser != null)
            {
                return _mapper.Map<UserDto, UserModel>(newUser);
            }

            return null;
        }
		#endregion

		#region Update User
		public async Task<int> UpdateUserAsync(UserModel model)
        {
			return await _userClient.UpdateAsync(_mapper.Map<UserModel, UserRequestDto>(model));
        }
		#endregion

		#region Delete User
		public async Task<int> DeleteUserAsync(string id)
        {
			return await _userClient.DeleteAsync(id);
        }
		#endregion

		#region Get User
		public async Task<UserModel?> GetUserAsync(string id, bool isDeep = false)
        {

			UserDto? user = await _userClient.GetAsync(id, isDeep);
			if(user != null)
			{
				return _mapper.Map<UserDto, UserModel>(user);
			}

            return null;
        }
		#endregion

		#region Get List Users
		public async Task<PagedDto<UserModel>> GetListUsersAsync(UserFilterDto filterDto)
        {
			PagedDto<UserDto>? pagedDto = await _userClient.GetListAsync(filterDto);

            if (pagedDto != null)
            {
                List<UserModel> list = new List<UserModel>();
                foreach (UserDto item in pagedDto.Data)
                {
                    list.Add(_mapper.Map<UserDto, UserModel>(item));
                }

                return new PagedDto<UserModel>(pagedDto.TotalRecords, list);
            }

            return new PagedDto<UserModel>(0, new List<UserModel>());
        }
		#endregion
	}
}