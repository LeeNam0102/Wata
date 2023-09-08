using Microsoft.EntityFrameworkCore;
using Wata.Commerce.Account.Domain.Models;

namespace Wata.Commerce.Account.Data.Context
{
    public partial class AccountContext : DbContext
    {
        public AccountContext() { }
        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
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
