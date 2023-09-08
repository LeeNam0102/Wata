using Microsoft.EntityFrameworkCore;
using Wata.Commerce.Sample.Domain.Models;

namespace Wata.Commerce.Sample.Data.Context
{
    public partial class SampleContext : DbContext
    {
        public SampleContext() { }
        public SampleContext(DbContextOptions<SampleContext> options) : base(options) { }

        public virtual DbSet<Abc> Abcs { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abc>(entity =>
            {
                entity.HasNoKey();
            });

            ViewModelBuilder(ref modelBuilder);
            StoreModelBuilder(ref modelBuilder);
        }
        */
    }
}
