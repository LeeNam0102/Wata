using Microsoft.EntityFrameworkCore;
using Wata.Commerce.Sample.Domain.Models;

namespace Wata.Commerce.Sample.Data.Context
{
    public partial class SampleContext : DbContext
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
