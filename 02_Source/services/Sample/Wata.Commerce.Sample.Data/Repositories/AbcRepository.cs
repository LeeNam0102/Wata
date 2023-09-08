using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wata.Commerce.Common.Data;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Sample.Data.Context;
using Wata.Commerce.Sample.Domain.Filters;
using Wata.Commerce.Sample.Domain.Models;
using Wata.Commerce.Sample.Domain.Repositories;

namespace Wata.Commerce.Sample.Data.Repositories
{
    public partial class AbcRepository : AbstractEfRepository<SampleContext, Abc>, IAbcRepository
    {
		public AbcRepository(SampleContext db, ILogger<AbcRepository> logger) : base(db, logger)
		{

		}

		private IQueryable<Abc> IncludeDeepObjects(IQueryable<Abc> query)
        {
            //return query.Include(o => o.ReferTable);
            return query;
        }

		#region Get By Id
		public async Task<Abc?> GetByIdAsync(int abcID, bool? isDeep = false)
        {
            IQueryable<Abc> query = _db.Abcs;
            query = query.Where(o => o.AbcID == abcID);

            if (isDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            return await query.SingleOrDefaultAsync();
        }
		#endregion

		#region Get List
		public async Task<PagedDto<Abc>> GetListAsync(AbcFilter filter)
        {
            int total = 0;
            IQueryable<Abc> query = _db.Abcs;

            //query where

            if (filter.IsOutputTotal)
            {
                var queryCount = query.Select(o => o.AbcID);
                total = await queryCount.CountAsync();
            }

            if (filter.IsDeep.Equals(true))
            {
                query = IncludeDeepObjects(query);
            }

            switch (filter.OrderBy)
            {
                default:
					query = filter.IsDescending ? query.OrderByDescending(o => o.AbcID) : query.OrderBy(o => o.AbcID);
                    break;
            }
            query = query.Skip(filter.GetSkip()).Take(filter.GetTake());

            return new PagedDto<Abc>(total, await query.ToListAsync());
        }
        #endregion
    }
}