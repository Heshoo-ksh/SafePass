﻿using Microsoft.EntityFrameworkCore;
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

          public async void CreateLogin(Login login)
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

          public async Task UpdateLogin(Login Login)
          {
               using (var context = dbContextFactory.CreateDbContext())
               {
                    context.Logins.Update(Login);
                    await context.SaveChangesAsync();
               }
          }

          public async Task DeleteLogin(Guid id)
          {
               using (var context = dbContextFactory.CreateDbContext())
               {
                    var Login = await context.Logins.FindAsync(id);
                    if (Login != null)
                    {
                         context.Logins.Remove(Login);
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