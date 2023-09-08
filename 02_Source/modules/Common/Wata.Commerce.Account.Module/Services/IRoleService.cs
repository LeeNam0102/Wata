using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Dtos;
using Wata.Commerce.Common.Module.Models.Role;

namespace Wata.Commerce.Common.Module.Services
{
    public interface IRoleService
    {
		Task<RoleModel?> InsertRoleAsync(RoleModel model);
		Task<int> UpdateRoleAsync(RoleModel model);
		Task<int> DeleteRoleAsync(string id);
		Task<RoleModel?> GetRoleAsync(string id, bool isDeep = false);
		Task<PagedDto<RoleModel>> GetListRolesAsync(RoleFilterDto filterDto);
		//put your code here
	}
}