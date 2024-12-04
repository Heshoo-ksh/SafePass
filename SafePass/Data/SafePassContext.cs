using Microsoft.EntityFrameworkCore;

namespace SafePass.Data
{
    public class SafePassContext : DbContext
    {
        public static readonly string SafePassDb = nameof(SafePassDb).ToLower();

        public SafePassContext(DbContextOptions<SafePassContext> options)
            : base(options) { }

        // Entities in the database
        public DbSet<Login> Logins { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Identity> Identities { get; set; }
        public DbSet<Note> Notes { get; set; }

          // No DbSet for Signup, as it is a frontend validation model
     }
}
