using Microsoft.Extensions.Logging;
using AutoMapper;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Client.Services;
using Wata.Commerce.Account.Dtos;
using Wata.Commerce.Common.Module.Models.Role;

namespace Wata.Commerce.Common.Module.Services
{
    public class RoleService : IRoleService
    {
		#region Fields
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		protected readonly IRoleClient _roleClient;
		#endregion

		#region Constructors
		public RoleService(
			ILogger<RoleService> logger,
			IMapper mapper,
			IRoleClient roleClient)
        {
			_logger = logger;
			_mapper = mapper;
            _roleClient = roleClient;
        }
		#endregion

		#region Insert Role
		public async Task<RoleModel?> InsertRoleAsync(RoleModel model)
        {
			RoleDto? newRole = await _roleClient.InsertAsync(_mapper.Map<RoleModel, RoleRequestDto>(model));

            if (newRole != null)
            {
                return _mapper.Map<RoleDto, RoleModel>(newRole);
            }

            return null;
        }
		#endregion

		#region Update Role
		public async Task<int> UpdateRoleAsync(RoleModel model)
        {
			return await _roleClient.UpdateAsync(_mapper.Map<RoleModel, RoleRequestDto>(model));
        }
		#endregion

		#region Delete Role
		public async Task<int> DeleteRoleAsync(string id)
        {
			return await _roleClient.DeleteAsync(id);
        }
		#endregion

		#region Get Role
		public async Task<RoleModel?> GetRoleAsync(string id, bool isDeep = false)
        {

			RoleDto? role = await _roleClient.GetAsync(id, isDeep);
			if(role != null)
			{
				return _mapper.Map<RoleDto, RoleModel>(role);
			}

            return null;
        }
		#endregion

		#region Get List Roles
		public async Task<PagedDto<RoleModel>> GetListRolesAsync(RoleFilterDto filterDto)
        {
			PagedDto<RoleDto>? pagedDto = await _roleClient.GetListAsync(filterDto);

            if (pagedDto != null)
            {
                List<RoleModel> list = new List<RoleModel>();
                foreach (RoleDto item in pagedDto.Data)
                {
                    list.Add(_mapper.Map<RoleDto, RoleModel>(item));
                }

                return new PagedDto<RoleModel>(pagedDto.TotalRecords, list);
            }

            return new PagedDto<RoleModel>(0, new List<RoleModel>());
        }
		#endregion
	}
}