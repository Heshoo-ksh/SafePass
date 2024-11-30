using Microsoft.EntityFrameworkCore;

namespace SafePass.Data
{
    public class UnifiedDbContext : DbContext
    {
        public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
    }
}
