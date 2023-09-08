using Microsoft.EntityFrameworkCore;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Data.Context
{
    public partial class AccountContext : DbContext
    {
        //public virtual DbSet<SampleView> SampleViews { get; set; } = null!;

        protected void ViewModelBuilder(ref ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<SampleView>(entity =>
            {
                entity.HasNoKey();

				entity.ToView("SampleViews", "Schema");

				entity.Property(e => e.SamepleId)
				.HasColumnName("SameplId");

				entity.Property(e => e.SamepleCode)
				.HasMaxLength(10)
				.HasColumnName("SamepleCode");

			});
            */
        }
    }
}
