using Microsoft.EntityFrameworkCore;
using SafePass.Data;

namespace SafePass.Services
{
     public class LoginService
     {
          private IDbContextFactory<SafePassContext> dbContextFactory;

          public LoginService(IDbContextFactory<SafePassContext> dbContextFactory)
          {
               this.dbContextFactory = dbContextFactory;
          }

          public async Task CreateLogin(Login login)
          {
               using (var context = dbContextFactory.CreateDbContext())
               {
                    context.Logins.Add(login);
                    await context.SaveChangesAsync();
               }
               //Navigation.NavigateTo("/");
          }
          public async Task<Login> GetLoginById(Guid id)
          {
               using (var context = dbContextFactory.CreateDbContext())
               {
                    return await context.Logins.FindAsync(id);
               }
          }

          public async Task UpdateLogin(Login login)
          {
               using (var context = dbContextFactory.CreateDbContext())
               {
                    context.Logins.Update(login);
                    await context.SaveChangesAsync();
               }
          }

          public async Task DeleteLogin(Guid id)
          {
               using (var context = dbContextFactory.CreateDbContext())
               {
                    var login = await context.Logins.FindAsync(id);
                    if (login != null)
                    {
                         context.Logins.Remove(login);
                         await context.SaveChangesAsync();
                    }
               }
          }

          public async Task<Login[]> GetAllLogins()
          {
               using (var context = dbContextFactory.CreateDbContext())
               {
                    return await context.Logins.ToArrayAsync();
               }
          }
     }
}
