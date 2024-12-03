using Microsoft.EntityFrameworkCore;
using SafePass.Data;

namespace SafePass.Services
{
     public class IdentityService
     {
          private readonly IDbContextFactory<SafePassContext> _dbContextFactory;

          public IdentityService(IDbContextFactory<SafePassContext> dbContextFactory)
          {
               _dbContextFactory = dbContextFactory;
          }

          public async Task CreateIdentity(Identity identity)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    context.Identities.Add(identity);
                    await context.SaveChangesAsync();
               }
          }

          public async Task<Identity?> GetIdentityById(Guid id)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    return await context.Identities.FindAsync(id);
               }
          }

          public async Task<Identity[]> GetAllIdentities()
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    return await context.Identities.ToArrayAsync();
               }
          }

          public async Task UpdateIdentity(Identity identity)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    context.Identities.Update(identity);
                    await context.SaveChangesAsync();
               }
          }

          public async Task DeleteIdentity(Guid id)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    var identity = await context.Identities.FindAsync(id);
                    if (identity != null)
                    {
                         context.Identities.Remove(identity);
                         await context.SaveChangesAsync();
                    }
               }
          }
     }
}
