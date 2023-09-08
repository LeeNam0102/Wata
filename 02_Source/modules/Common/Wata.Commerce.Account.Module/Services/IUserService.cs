using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Dtos;
using Wata.Commerce.Common.Module.Models.User;

namespace Wata.Commerce.Common.Module.Services
{
    public interface IUserService
    {
		Task<UserModel?> InsertUserAsync(UserModel model);
		Task<int> UpdateUserAsync(UserModel model);
		Task<int> DeleteUserAsync(string id);
		Task<UserModel?> GetUserAsync(string id, bool isDeep = false);
		Task<PagedDto<UserModel>> GetListUsersAsync(UserFilterDto filterDto);
		//put your code here
	}
}