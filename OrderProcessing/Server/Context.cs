using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace OrderProcessing.Server;

/*
 * dotnet ef migrations add OrderProcessing_v1
 */

public class Context : DbContext
{
    public DbSet<Models.Order> Orders => Set<Models.Order>();
    public DbSet<Models.OrderLine> OrderLines => Set<Models.OrderLine>();

    public Context() { }
    public Context(DbContextOptions<Context> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var name = modelBuilder.Entity(entity.Name).Metadata.ClrType.Name;
            modelBuilder.Entity(entity.Name).ToTable($"Order.{name}");
        };

        base.OnModelCreating(modelBuilder);
    }

    public static void Initialize(Context _context)
    {
        if (_context.Database.GetPendingMigrations().Any() == false)
            return;

        _context.Database.Migrate();
    }
}