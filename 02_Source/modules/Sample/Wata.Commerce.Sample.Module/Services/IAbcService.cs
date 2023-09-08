using Wata.Commerce.Common.Objects;
using Wata.Commerce.Sample.Dtos;
using Wata.Commerce.Sample.Module.Models.Abc;

namespace Wata.Commerce.Sample.Module.Services
{
    public interface IAbcService
    {
		Task<AbcModel?> InsertAbcAsync(AbcModel model);
		Task<int> UpdateAbcAsync(AbcModel model);
		Task<int> DeleteAbcAsync(int abcID);
		Task<AbcModel?> GetAbcAsync(int abcID, bool isDeep = false);
		Task<PagedDto<AbcModel>> GetListAbcsAsync(AbcFilterDto filterDto);
		//put your code here
	}
}