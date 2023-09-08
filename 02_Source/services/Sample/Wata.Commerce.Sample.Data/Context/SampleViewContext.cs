using Microsoft.EntityFrameworkCore;
using Wata.Commerce.Sample.Domain.Models;

namespace Wata.Commerce.Sample.Data.Context
{
    public partial class SampleContext : DbContext
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
