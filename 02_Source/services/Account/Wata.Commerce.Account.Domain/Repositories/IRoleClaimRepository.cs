using System.Threading.Tasks;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Domain.Repositories
{
    public interface IRoleClaimRepository : IRepository<RoleClaim>
    {
		Task<RoleClaim?> GetByIdAsync(int id, bool? isDeep = false);
		Task<PagedDto<RoleClaim>> GetListAsync(RoleClaimFilter filter);
		//put your code here
    }
}