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


        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(u => u.UserName == username);

                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user; // Password matches
                }


                return user;
            }
        }
    }
}
