using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Account.Data.Context;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Domain.Models;
using Wata.Commerce.Account.Domain.Repositories;

namespace Wata.Commerce.Account.Data.Repositories
{
    public partial class UserRoleRepository : AbstractEfRepository<AccountContext, UserRole>, IUserRoleRepository
    {
		public UserRoleRepository(AccountContext db, ILogger<UserRoleRepository> logger) : base(db, logger)
		{

		}

		private IQueryable<UserRole> IncludeDeepObjects(IQueryable<UserRole> query)
        {
            //return query.Include(o => o.ReferTable);
            return query;
        }

		#region Get By Id
		public async Task<UserRole?> GetByIdAsync(string userId, string roleId, bool? isDeep = false)
        {
            IQueryable<UserRole> query = _db.UserRoles;
            query = query.Where(o => o.UserId == userId && o.RoleId == roleId);

            if (isDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            return await query.SingleOrDefaultAsync();
        }
		#endregion

		#region Get List
		public async Task<PagedDto<UserRole>> GetListAsync(UserRoleFilter filter)
        {
            int total = 0;
            IQueryable<UserRole> query = _db.UserRoles;

            //query where

            if (filter.IsOutputTotal)
            {
                var queryCount = query.Select(o => o.UserId);
                total = await queryCount.CountAsync();
            }

            if (filter.IsDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            switch (filter.OrderBy)
            {
                default:
					query = filter.IsDescending ? query.OrderByDescending(o => o.UserId) : query.OrderBy(o => o.UserId);
                    break;
            }
            query = query.Skip(filter.GetSkip()).Take(filter.GetTake());

            return new PagedDto<UserRole>(total, await query.ToListAsync());
        }
        #endregion
    }
}