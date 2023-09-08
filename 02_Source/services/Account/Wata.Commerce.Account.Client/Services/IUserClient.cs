using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Client.Services
{
    public interface IUserClient
    {
		Task<UserDto?> InsertAsync(UserRequestDto requestDto);
		Task<int> UpdateAsync(UserRequestDto requestDto);
		Task<int> DeleteAsync(string id);
		Task<UserDto?> GetAsync(string id, bool? isDeep = null);
		Task<PagedDto<UserDto>?> GetListAsync(UserFilterDto filterDto);
		//Task<string> Export(UserFilterDto filterDto, string exportType);
		//put your code here
	}
}