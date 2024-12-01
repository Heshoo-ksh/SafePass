using Microsoft.EntityFrameworkCore;
using SafePass.Data;

namespace SafePass.Services
{
    public class UserService
    {
        private readonly IDbContextFactory<SafePassContext> _dbContextFactory;

        public UserService(IDbContextFactory<SafePassContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Validates user credentials for login.
        /// </summary>
        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(u => u.UserName == username);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user; // Password matches
                }

                return null; // Invalid credentials
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        public async Task<bool> RegisterUserAsync(User signup)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                // Check if the username already exists
                if (await context.Users.AnyAsync(u => u.UserName == signup.UserName))
                {
                    throw new Exception("A user with this username already exists.");
                }

                // Map Signup to User model
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = signup.UserName,
                    Password = HashPassword(signup.Password), // Hash password before saving
                    SecurityQuestion1 = signup.SecurityQuestion1,
                    SecurityAnswer1 = HashPassword(signup.SecurityAnswer1), // Hash answers
                    SecurityQuestion2 = signup.SecurityQuestion2,
                    SecurityAnswer2 = HashPassword(signup.SecurityAnswer2),
                    SecurityQuestion3 = signup.SecurityQuestion3,
                    SecurityAnswer3 = HashPassword(signup.SecurityAnswer3)
                };

                // Add the new user to the database
                context.Users.Add(newUser);
                await context.SaveChangesAsync();

                return true;
            }
        }

        /// <summary>
        /// Hashes a given string (password or security answer) using BCrypt.
        /// </summary>
        private string HashPassword(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }

       
    }
}
