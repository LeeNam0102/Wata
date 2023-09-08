using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Business.Services
{
    public interface IRoleService
    {
		Task<RoleDto?> InsertRoleAsync(RoleRequestDto requestDto);
		Task<int> UpdateRoleAsync(RoleRequestDto requestDto);
		Task<int> DeleteRoleAsync(string id);
		Task<RoleDto?> GetRoleAsync(string id, bool isDeep = false);
		Task<PagedDto<RoleDto>> GetListRolesAsync(RoleFilterDto filterDto);
		//put your code here
	}
}