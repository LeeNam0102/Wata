using Microsoft.Extensions.Logging;
using AutoMapper;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Utility;
using Wata.Commerce.Sample.Domain.Models;
using Wata.Commerce.Sample.Domain.Filters;
using Wata.Commerce.Sample.Domain.Repositories;
using Wata.Commerce.Sample.Dtos;

namespace Wata.Commerce.Sample.Business.Services
{
    public class AbcService : IAbcService
    {
		#region Fields
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		protected readonly IAbcRepository _abcRepository;
		#endregion

		#region Constructors
		public AbcService(
			ILogger<AbcService> logger,
			IMapper mapper,
			IAbcRepository abcRepository)
        {
			_logger = logger;
			_mapper = mapper;
            _abcRepository = abcRepository;
        }
		#endregion

		#region Insert Abc
		public async Task<AbcDto?> InsertAbcAsync(AbcRequestDto dto)
        {
			Abc abc = new Abc();
			abc.AbcID = dto.AbcID;
			abc.Name = dto.Name;
			abc.CreateDate = SystemUtil.GetDateTime();
			abc.CreateBy = dto.GetUserID();

			Abc? newAbc = await _abcRepository.InsertAsync(abc);

			if (newAbc != null)
			{
				dto.AbcID = newAbc.AbcID;
		
				return _mapper.Map<Abc, AbcDto>(newAbc);
			}

			return null;
        }
		#endregion

		#region Update Abc
		public async Task<int> UpdateAbcAsync(AbcRequestDto dto)
        {

			Abc? abc = await _abcRepository.GetByIdAsync(dto.AbcID);
			if(abc != null)
			{
				abc.Name = dto.Name;
				abc.UpdateDate = SystemUtil.GetDateTime();
				abc.UpdateBy = dto.GetUserID();

				return await _abcRepository.UpdateAsync(abc);
			}

            return 0;
        }
		#endregion

		#region Delete Abc
		public async Task<int> DeleteAbcAsync(int abcID)
        {

			return await _abcRepository.DeleteAsync(abcID);
        }
		#endregion

		#region Get Abc
		public async Task<AbcDto?> GetAbcAsync(int abcID, bool isDeep = false)
        {

			Abc? abc = await _abcRepository.GetByIdAsync(abcID, isDeep);
			if(abc != null)
			{
				return _mapper.Map<Abc, AbcDto>(abc);
			}

            return null;
        }
		#endregion

		#region Get List Abcs
		public async Task<PagedDto<AbcDto>> GetListAbcsAsync(AbcFilterDto filterDto)
        {
			PagedDto<Abc> dt = await _abcRepository.GetListAsync(_mapper.Map<AbcFilterDto, AbcFilter>(filterDto));

			List<AbcDto> dtos = new List<AbcDto>();
			foreach(Abc item in dt.Data)
            {
				dtos.Add(_mapper.Map<Abc, AbcDto>(item));
            }

			return new PagedDto<AbcDto>(dt.TotalRecords, dtos);
        }
		#endregion
	}
}