using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Xml;

namespace SafePass.Data
{
     public class SafePassContext : DbContext
     {
          public static readonly string SafePassDb = nameof(SafePassDb).ToLower();

          public SafePassContext(DbContextOptions<SafePassContext> options)
          : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

    }
}
