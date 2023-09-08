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
    public partial class UserLoginRepository : AbstractEfRepository<AccountContext, UserLogin>, IUserLoginRepository
    {
		public UserLoginRepository(AccountContext db, ILogger<UserLoginRepository> logger) : base(db, logger)
		{

		}

		private IQueryable<UserLogin> IncludeDeepObjects(IQueryable<UserLogin> query)
        {
            //return query.Include(o => o.ReferTable);
            return query;
        }

		#region Get By Id
		public async Task<UserLogin?> GetByIdAsync(string loginProvider, string providerKey, bool? isDeep = false)
        {
            IQueryable<UserLogin> query = _db.UserLogins;
            query = query.Where(o => o.LoginProvider == loginProvider && o.ProviderKey == providerKey);

            if (isDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            return await query.SingleOrDefaultAsync();
        }
		#endregion

		#region Get List
		public async Task<PagedDto<UserLogin>> GetListAsync(UserLoginFilter filter)
        {
            int total = 0;
            IQueryable<UserLogin> query = _db.UserLogins;

            //query where

            if (filter.IsOutputTotal)
            {
                var queryCount = query.Select(o => o.LoginProvider);
                total = await queryCount.CountAsync();
            }

            if (filter.IsDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            switch (filter.OrderBy)
            {
                default:
					query = filter.IsDescending ? query.OrderByDescending(o => o.LoginProvider) : query.OrderBy(o => o.LoginProvider);
                    break;
            }
            query = query.Skip(filter.GetSkip()).Take(filter.GetTake());

            return new PagedDto<UserLogin>(total, await query.ToListAsync());
        }
        #endregion
    }
}