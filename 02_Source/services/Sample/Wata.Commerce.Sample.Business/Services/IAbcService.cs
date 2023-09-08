using Wata.Commerce.Common.Objects;
using Wata.Commerce.Sample.Dtos;

namespace Wata.Commerce.Sample.Business.Services
{
    public interface IAbcService
    {
		Task<AbcDto?> InsertAbcAsync(AbcRequestDto requestDto);
		Task<int> UpdateAbcAsync(AbcRequestDto requestDto);
		Task<int> DeleteAbcAsync(int abcID);
		Task<AbcDto?> GetAbcAsync(int abcID, bool isDeep = false);
		Task<PagedDto<AbcDto>> GetListAbcsAsync(AbcFilterDto filterDto);
		//put your code here
	}
}