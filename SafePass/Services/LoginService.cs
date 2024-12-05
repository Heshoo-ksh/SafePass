using Microsoft.EntityFrameworkCore;
using SafePass.Data;

namespace SafePass.Services
{
    public class LoginService
    {
        private readonly IDbContextFactory<SafePassContext> _dbContextFactory;

        public LoginService(IDbContextFactory<SafePassContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateLogin(Login login)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Logins.Add(login);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Login?> GetLoginById(Guid id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Logins.FindAsync(id);
            }
        }

        public async Task<Login[]> GetAllLogins()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Logins.ToArrayAsync();
            }
        }

        public async Task UpdateLogin(Login login)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                // Attach the login to the context and mark it as modified
                context.Logins.Update(login);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteLogin(Guid id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var login = await context.Logins.FindAsync(id);
                if (login != null)
                {
                    context.Logins.Remove(login);
                    await context.SaveChangesAsync();
                }
            }
        }
        
    }
}
