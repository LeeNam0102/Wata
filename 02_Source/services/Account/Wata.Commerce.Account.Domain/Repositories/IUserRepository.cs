using System.Threading.Tasks;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
		Task<User?> GetByIdAsync(string id, bool? isDeep = false);
		Task<PagedDto<User>> GetListAsync(UserFilter filter);
		//put your code here
    }
}