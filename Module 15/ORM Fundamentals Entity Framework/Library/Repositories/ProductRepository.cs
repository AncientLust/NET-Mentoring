using Library.Database_connections;
using Library.Models;

namespace Library.Repositories;

public class ProductRepository
{
    public void Insert(Product product)
    {
        using var context = new SqlServerContext();
        context.Products.Add(product);
        context.SaveChanges();
    }

    public void Update(Product product)
    {
        using var context = new SqlServerContext();

        if (!context.Products.Contains(product))
            throw new ArgumentException($"Non existent ProductId {product.Id}");
        
        context.Products.Update(product);
        context.SaveChanges();
    }

    public List<Product> SelectAll()
    {
        using var context = new SqlServerContext();
        return context.Products.ToList();
    }

    public Product? SelectById(int productId)
    {
        using var context = new SqlServerContext();
        return context.Products.Find(productId);
    }

    public void DeleteById(int productId)
    {
        using var context = new SqlServerContext();
        var product = context.Products.Find(productId);

        if (product is null)
            throw new ArgumentException($"Non existent ProductId {productId}");

        context.Products.Remove(product);
        context.SaveChanges();
    }

    public void DeleteAll()
    {
        using var context = new SqlServerContext();
        var products = context.Products.ToList();
        context.Products.RemoveRange(products);
        context.SaveChanges();
    }
}
