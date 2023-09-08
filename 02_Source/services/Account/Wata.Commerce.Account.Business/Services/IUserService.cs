using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Business.Services
{
    public interface IUserService
    {
		Task<UserDto?> InsertUserAsync(UserRequestDto requestDto);
		Task<int> UpdateUserAsync(UserRequestDto requestDto);
		Task<int> DeleteUserAsync(string id);
		Task<UserDto?> GetUserAsync(string id, bool isDeep = false);
		Task<PagedDto<UserDto>> GetListUsersAsync(UserFilterDto filterDto);
		//put your code here
	}
}