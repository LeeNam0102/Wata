using System.Threading.Tasks;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Domain.Repositories
{
    public interface IUserTokenRepository : IRepository<UserToken>
    {
		Task<UserToken?> GetByIdAsync(string userId, string loginProvider, string name, bool? isDeep = false);
		Task<PagedDto<UserToken>> GetListAsync(UserTokenFilter filter);
		//put your code here
    }
}