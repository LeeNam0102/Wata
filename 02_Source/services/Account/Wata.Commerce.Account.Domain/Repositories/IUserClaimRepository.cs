using System.Threading.Tasks;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Domain.Repositories
{
    public interface IUserClaimRepository : IRepository<UserClaim>
    {
		Task<UserClaim?> GetByIdAsync(int id, bool? isDeep = false);
		Task<PagedDto<UserClaim>> GetListAsync(UserClaimFilter filter);
		//put your code here
    }
}