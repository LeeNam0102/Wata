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
    public partial class RoleRepository : AbstractEfRepository<AccountContext, Role>, IRoleRepository
    {
		public RoleRepository(AccountContext db, ILogger<RoleRepository> logger) : base(db, logger)
		{

		}

		private IQueryable<Role> IncludeDeepObjects(IQueryable<Role> query)
        {
            //return query.Include(o => o.ReferTable);
            return query;
        }

		#region Get By Id
		public async Task<Role?> GetByIdAsync(string id, bool? isDeep = false)
        {
            IQueryable<Role> query = _db.Roles;
            query = query.Where(o => o.Id == id);

            if (isDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            return await query.SingleOrDefaultAsync();
        }
		#endregion

		#region Get List
		public async Task<PagedDto<Role>> GetListAsync(RoleFilter filter)
        {
            int total = 0;
            IQueryable<Role> query = _db.Roles;

            //query where

            if (filter.IsOutputTotal)
            {
                var queryCount = query.Select(o => o.Id);
                total = await queryCount.CountAsync();
            }

            if (filter.IsDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            switch (filter.OrderBy)
            {
                default:
					query = filter.IsDescending ? query.OrderByDescending(o => o.Id) : query.OrderBy(o => o.Id);
                    break;
            }
            query = query.Skip(filter.GetSkip()).Take(filter.GetTake());

            return new PagedDto<Role>(total, await query.ToListAsync());
        }
        #endregion
    }
}