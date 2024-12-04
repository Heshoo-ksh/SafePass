using Microsoft.EntityFrameworkCore;
using SafePass.Data;
using System.Threading.Tasks;

namespace SafePass.Services
{
     public class NoteService
     {
          private readonly IDbContextFactory<SafePassContext> _dbContextFactory;

          public NoteService(IDbContextFactory<SafePassContext> dbContextFactory)
          {
               _dbContextFactory = dbContextFactory;
          }

          public async Task CreateNote(Note note)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    context.Notes.Add(note);
                    await context.SaveChangesAsync();
               }
          }

          public async Task<Note?> GetNoteById(Guid id)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    return await context.Notes.FindAsync(id);
               }
          }

          public async Task<Note[]> GetAllNotes()
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    return await context.Notes.ToArrayAsync();
               }
          }

          public async Task UpdateNote(Note note)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    context.Notes.Update(note);
                    await context.SaveChangesAsync();
               }
          }

          public async Task DeleteNote(Guid id)
          {
               using (var context = _dbContextFactory.CreateDbContext())
               {
                    var note = await context.Notes.FindAsync(id);
                    if (note != null)
                    {
                         context.Notes.Remove(note);
                         await context.SaveChangesAsync();
                    }
               }
          }
     }
}
