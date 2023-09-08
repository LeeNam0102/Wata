using Wata.Commerce.Common.Objects;
using Wata.Commerce.Sample.Dtos;

namespace Wata.Commerce.Sample.Client.Services
{
    public interface IAbcClient
    {
		Task<AbcDto?> InsertAsync(AbcRequestDto requestDto);
		Task<int> UpdateAsync(AbcRequestDto requestDto);
		Task<int> DeleteAsync(int abcID);
		Task<AbcDto?> GetAsync(int abcID, bool? isDeep = null);
		Task<PagedDto<AbcDto>?> GetListAsync(AbcFilterDto filterDto);
		//Task<string> Export(AbcFilterDto filterDto, string exportType);
		//put your code here
	}
}