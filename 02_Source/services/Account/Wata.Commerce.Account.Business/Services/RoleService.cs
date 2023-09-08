using Microsoft.Extensions.Logging;
using AutoMapper;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Utility;
using Wata.Commerce.Account.Domain.Models;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Repositories;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Business.Services
{
    public class RoleService : IRoleService
    {
		#region Fields
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		protected readonly IRoleRepository _roleRepository;
		#endregion

		#region Constructors
		public RoleService(
			ILogger<RoleService> logger,
			IMapper mapper,
			IRoleRepository roleRepository)
        {
			_logger = logger;
			_mapper = mapper;
            _roleRepository = roleRepository;
        }
		#endregion

		#region Insert Role
		public async Task<RoleDto?> InsertRoleAsync(RoleRequestDto dto)
        {
			Role role = new Role();
			role.Id = dto.Id;
			role.ConcurrencyStamp = dto.ConcurrencyStamp;
			role.Name = dto.Name;
			role.NormalizedName = dto.NormalizedName;

			Role? newRole = await _roleRepository.InsertAsync(role);

			if (newRole != null)
			{
				return _mapper.Map<Role, RoleDto>(newRole);
			}

			return null;
        }
		#endregion

		#region Update Role
		public async Task<int> UpdateRoleAsync(RoleRequestDto dto)
        {

			Role? role = await _roleRepository.GetByIdAsync(dto.Id);
			if(role != null)
			{
				role.ConcurrencyStamp = dto.ConcurrencyStamp;
				role.Name = dto.Name;
				role.NormalizedName = dto.NormalizedName;

				return await _roleRepository.UpdateAsync(role);
			}

            return 0;
        }
		#endregion

		#region Delete Role
		public async Task<int> DeleteRoleAsync(string id)
        {

			return await _roleRepository.DeleteAsync(id);
        }
		#endregion

		#region Get Role
		public async Task<RoleDto?> GetRoleAsync(string id, bool isDeep = false)
        {

			Role? role = await _roleRepository.GetByIdAsync(id, isDeep);
			if(role != null)
			{
				return _mapper.Map<Role, RoleDto>(role);
			}

            return null;
        }
		#endregion

		#region Get List Roles
		public async Task<PagedDto<RoleDto>> GetListRolesAsync(RoleFilterDto filterDto)
        {
			PagedDto<Role> dt = await _roleRepository.GetListAsync(_mapper.Map<RoleFilterDto, RoleFilter>(filterDto));

			List<RoleDto> dtos = new List<RoleDto>();
			foreach(Role item in dt.Data)
            {
				dtos.Add(_mapper.Map<Role, RoleDto>(item));
            }

			return new PagedDto<RoleDto>(dt.TotalRecords, dtos);
        }
		#endregion
	}
}