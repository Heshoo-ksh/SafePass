using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace SafePass.Data
{
    public class UserContext : DbContext
    {
        /// <summary>
        /// The name of the contacts database, derived from the field name and converted to lowercase.
        /// </summary>
        public static readonly string UsersDB = nameof(UsersDB).ToLower();

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            // Log the creation of a new context instance with its unique identifier to the debug output.
            Debug.WriteLine($"{ContextId} context created.");
        }
        /// <summary>
        /// A collection representing a list of contact objects.
        /// </summary>
        public DbSet<User>? Users { get; set; }

    }
}
