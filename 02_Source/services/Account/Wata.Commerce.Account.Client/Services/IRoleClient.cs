using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Client.Services
{
    public interface IRoleClient
    {
		Task<RoleDto?> InsertAsync(RoleRequestDto requestDto);
		Task<int> UpdateAsync(RoleRequestDto requestDto);
		Task<int> DeleteAsync(string id);
		Task<RoleDto?> GetAsync(string id, bool? isDeep = null);
		Task<PagedDto<RoleDto>?> GetListAsync(RoleFilterDto filterDto);
		//Task<string> Export(RoleFilterDto filterDto, string exportType);
		//put your code here
	}
}