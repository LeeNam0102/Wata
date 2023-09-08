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
    public partial class RoleClaimRepository : AbstractEfRepository<AccountContext, RoleClaim>, IRoleClaimRepository
    {
		public RoleClaimRepository(AccountContext db, ILogger<RoleClaimRepository> logger) : base(db, logger)
		{

		}

		private IQueryable<RoleClaim> IncludeDeepObjects(IQueryable<RoleClaim> query)
        {
            //return query.Include(o => o.ReferTable);
            return query;
        }

		#region Get By Id
		public async Task<RoleClaim?> GetByIdAsync(int id, bool? isDeep = false)
        {
            IQueryable<RoleClaim> query = _db.RoleClaims;
            query = query.Where(o => o.Id == id);

            if (isDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            return await query.SingleOrDefaultAsync();
        }
		#endregion

		#region Get List
		public async Task<PagedDto<RoleClaim>> GetListAsync(RoleClaimFilter filter)
        {
            int total = 0;
            IQueryable<RoleClaim> query = _db.RoleClaims;

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

            return new PagedDto<RoleClaim>(total, await query.ToListAsync());
        }
        #endregion
    }
}