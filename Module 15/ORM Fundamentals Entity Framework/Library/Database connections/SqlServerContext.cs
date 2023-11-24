using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Database_connections;

public class SqlServerContext : DbContext
{
    private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=orm_module_entity_framework;Integrated Security=True;Connect Timeout=30";
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder oprionsBuilder)
    {
        oprionsBuilder.UseSqlServer(ConnectionString);
        base.OnConfiguring(oprionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable(nameof(Product));
        modelBuilder.Entity<Order>().ToTable(nameof(Order));

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();
    }
}