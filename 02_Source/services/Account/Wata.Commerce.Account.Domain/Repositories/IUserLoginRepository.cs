using System.Threading.Tasks;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Domain.Repositories
{
    public interface IUserLoginRepository : IRepository<UserLogin>
    {
		Task<UserLogin?> GetByIdAsync(string loginProvider, string providerKey, bool? isDeep = false);
		Task<PagedDto<UserLogin>> GetListAsync(UserLoginFilter filter);
		//put your code here
    }
}