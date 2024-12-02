using Microsoft.EntityFrameworkCore;
using SafePass.Data;

namespace SafePass.Services
{
     public class CreditCardService
     {
          private readonly IDbContextFactory<SafePassContext> _dbContextFactory;

          public CreditCardService(IDbContextFactory<SafePassContext> dbContextFactory)
          {
               _dbContextFactory = dbContextFactory;
          }

          public async Task CreateCreditCard(CreditCard creditCard)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    context.CreditCards.Add(creditCard);
                    await context.SaveChangesAsync();
               }
          }

          public async Task<CreditCard?> GetCreditCardById(Guid id)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    return await context.CreditCards.FindAsync(id);
               }
          }

          public async Task<CreditCard[]> GetAllCreditCards()
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    return await context.CreditCards.ToArrayAsync();
               }
          }

          public async Task UpdateCreditCard(CreditCard creditCard)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    context.CreditCards.Update(creditCard);
                    await context.SaveChangesAsync();
               }
          }

          public async Task DeleteCreditCard(Guid id)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    var creditCard = await context.CreditCards.FindAsync(id);
                    if (creditCard != null)
                    {
                         context.CreditCards.Remove(creditCard);
                         await context.SaveChangesAsync();
                    }
               }
          }
     }
}
