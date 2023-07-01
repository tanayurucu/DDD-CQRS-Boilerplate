using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence;

public class CleanArchitectureDbContext : DbContext
{
    public CleanArchitectureDbContext(DbContextOptions<CleanArchitectureDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(PersistenceAssembly.Assembly);
    }
}