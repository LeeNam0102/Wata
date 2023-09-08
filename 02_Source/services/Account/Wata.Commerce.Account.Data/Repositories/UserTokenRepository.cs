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
    public partial class UserTokenRepository : AbstractEfRepository<AccountContext, UserToken>, IUserTokenRepository
    {
		public UserTokenRepository(AccountContext db, ILogger<UserTokenRepository> logger) : base(db, logger)
		{

		}

		private IQueryable<UserToken> IncludeDeepObjects(IQueryable<UserToken> query)
        {
            //return query.Include(o => o.ReferTable);
            return query;
        }

		#region Get By Id
		public async Task<UserToken?> GetByIdAsync(string userId, string loginProvider, string name, bool? isDeep = false)
        {
            IQueryable<UserToken> query = _db.UserTokens;
            query = query.Where(o => o.UserId == userId && o.LoginProvider == loginProvider && o.Name == name);

            if (isDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            return await query.SingleOrDefaultAsync();
        }
		#endregion

		#region Get List
		public async Task<PagedDto<UserToken>> GetListAsync(UserTokenFilter filter)
        {
            int total = 0;
            IQueryable<UserToken> query = _db.UserTokens;

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

            return new PagedDto<UserToken>(total, await query.ToListAsync());
        }
        #endregion
    }
}