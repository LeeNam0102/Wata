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
    public partial class UserClaimRepository : AbstractEfRepository<AccountContext, UserClaim>, IUserClaimRepository
    {
		public UserClaimRepository(AccountContext db, ILogger<UserClaimRepository> logger) : base(db, logger)
		{

		}

		private IQueryable<UserClaim> IncludeDeepObjects(IQueryable<UserClaim> query)
        {
            //return query.Include(o => o.ReferTable);
            return query;
        }

		#region Get By Id
		public async Task<UserClaim?> GetByIdAsync(int id, bool? isDeep = false)
        {
            IQueryable<UserClaim> query = _db.UserClaims;
            query = query.Where(o => o.Id == id);

            if (isDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            return await query.SingleOrDefaultAsync();
        }
		#endregion

		#region Get List
		public async Task<PagedDto<UserClaim>> GetListAsync(UserClaimFilter filter)
        {
            int total = 0;
            IQueryable<UserClaim> query = _db.UserClaims;

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

            return new PagedDto<UserClaim>(total, await query.ToListAsync());
        }
        #endregion
    }
}