using Microsoft.EntityFrameworkCore;

namespace Wata.Commerce.Sample.IntegrationTest.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}