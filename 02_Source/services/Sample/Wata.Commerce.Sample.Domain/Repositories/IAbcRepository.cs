using System.Threading.Tasks;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Sample.Domain.Filters;
using Wata.Commerce.Sample.Domain.Models;

namespace Wata.Commerce.Sample.Domain.Repositories
{
    public interface IAbcRepository : IRepository<Abc>
    {
		Task<Abc?> GetByIdAsync(int abcID, bool? isDeep = false);
		Task<PagedDto<Abc>> GetListAsync(AbcFilter filter);
		//put your code here
    }
}