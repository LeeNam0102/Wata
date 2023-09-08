using System.Threading.Tasks;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Domain.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
		Task<UserRole?> GetByIdAsync(string userId, string roleId, bool? isDeep = false);
		Task<PagedDto<UserRole>> GetListAsync(UserRoleFilter filter);
		//put your code here
    }
}