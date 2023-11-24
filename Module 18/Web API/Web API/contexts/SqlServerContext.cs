using Microsoft.EntityFrameworkCore;
using Web_API.Models;

namespace Web_API.contexts;

public class SqlServerContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }
}