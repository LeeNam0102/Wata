using Microsoft.EntityFrameworkCore;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Data.Context
{
    public partial class AccountContext : DbContext
    {
        //public virtual DbSet<AbcResult> AbcResults { get; set; } = null!;

		protected void StoreModelBuilder(ref ModelBuilder modelBuilder)
		{
			/*
			modelBuilder.Entity<AbcResult>(entity =>
			{
				entity.HasNoKey();
			});
			*/
		}
    }
}
