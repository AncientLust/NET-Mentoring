using Dapper;
using Library.Interfaces;
using Library.Models;

namespace Library.Repositories;

public class ProductRepository
{
    private readonly IDatabaseConnector _databaseConnector;

    public ProductRepository(IDatabaseConnector databaseConnector)
    {
        _databaseConnector = databaseConnector;
    }

    public void Insert(Product product)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = """
                           INSERT INTO dbo.product
                           (Id, Name, Description, Weight, Height, Width, Length)
                           VALUES (
                              @Id,
                              @Name,
                              @Description,
                              @Weight,
                              @Height,
                              @Width,
                              @Length
                           )
                           """;

        connection.Execute(sql, product);
    }

    public void Update(Product product)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = """
                           UPDATE dbo.product
                           SET
                               Name = @Name,
                               Description = @Description,
                               Weight = @Weight,
                               Height = @Height,
                               Width = @Width,
                               Length = @Length
                           WHERE Id = @Id
                           """;

        int updatedRecords = connection.Execute(sql, product);

        if (updatedRecords == 0) 
            throw new ArgumentException($"Non existent ProductId {product.Id}");
    }

    public List<Product> SelectAll()
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM dbo.product";

        return connection.Query<Product>(sql).ToList();
    }

    public Product? SelectById(int productId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM dbo.product WHERE Id = @Id";

        return connection.QueryFirstOrDefault<Product>(sql, new {Id = productId});
    }

    public void DeleteById(int productId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE dbo.product WHERE Id = @Id";

        int updatedRecords = connection.Execute(sql, new {Id = productId});

        if (updatedRecords == 0)
            throw new ArgumentException($"Non existent ProductId {productId}");
    }

    public void DeleteAll()
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE dbo.product";

        connection.Execute(sql);
    }
}
