using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SafePass.Data;

public class UnifiedDbContextFactory : IDesignTimeDbContextFactory<UnifiedDbContext>
{
    public UnifiedDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UnifiedDbContext>();
        optionsBuilder.UseSqlite("Data Source=unified_database.db");

        return new UnifiedDbContext(optionsBuilder.Options);
    }
}
