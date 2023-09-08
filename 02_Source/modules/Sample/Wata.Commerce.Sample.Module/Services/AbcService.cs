using Microsoft.Extensions.Logging;
using AutoMapper;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Sample.Client.Services;
using Wata.Commerce.Sample.Dtos;
using Wata.Commerce.Sample.Module.Models.Abc;

namespace Wata.Commerce.Sample.Module.Services
{
    public class AbcService : IAbcService
    {
		#region Fields
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		protected readonly IAbcClient _abcClient;
		#endregion

		#region Constructors
		public AbcService(
			ILogger<AbcService> logger,
			IMapper mapper,
			IAbcClient abcClient)
        {
			_logger = logger;
			_mapper = mapper;
            _abcClient = abcClient;
        }
		#endregion

		#region Insert Abc
		public async Task<AbcModel?> InsertAbcAsync(AbcModel model)
        {
			AbcDto? newAbc = await _abcClient.InsertAsync(_mapper.Map<AbcModel, AbcRequestDto>(model));

            if (newAbc != null)
            {
                return _mapper.Map<AbcDto, AbcModel>(newAbc);
            }

            return null;
        }
		#endregion

		#region Update Abc
		public async Task<int> UpdateAbcAsync(AbcModel model)
        {
			return await _abcClient.UpdateAsync(_mapper.Map<AbcModel, AbcRequestDto>(model));
        }
		#endregion

		#region Delete Abc
		public async Task<int> DeleteAbcAsync(int abcID)
        {
			return await _abcClient.DeleteAsync(abcID);
        }
		#endregion

		#region Get Abc
		public async Task<AbcModel?> GetAbcAsync(int abcID, bool isDeep = false)
        {

			AbcDto? abc = await _abcClient.GetAsync(abcID, isDeep);
			if(abc != null)
			{
				return _mapper.Map<AbcDto, AbcModel>(abc);
			}

            return null;
        }
		#endregion

		#region Get List Abcs
		public async Task<PagedDto<AbcModel>> GetListAbcsAsync(AbcFilterDto filterDto)
        {
			PagedDto<AbcDto>? pagedDto = await _abcClient.GetListAsync(filterDto);

            if (pagedDto != null)
            {
                List<AbcModel> list = new List<AbcModel>();
                foreach (AbcDto item in pagedDto.Data)
                {
                    list.Add(_mapper.Map<AbcDto, AbcModel>(item));
                }

                return new PagedDto<AbcModel>(pagedDto.TotalRecords, list);
            }

            return new PagedDto<AbcModel>(0, new List<AbcModel>());
        }
		#endregion
	}
}