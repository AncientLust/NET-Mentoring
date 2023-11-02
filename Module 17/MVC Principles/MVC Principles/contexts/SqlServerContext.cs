using Microsoft.EntityFrameworkCore;
using MVC_Principles.Models;

namespace MVC_Principles.contexts;

public class SqlServerContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }
}