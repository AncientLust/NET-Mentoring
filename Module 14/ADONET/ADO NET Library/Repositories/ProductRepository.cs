using ADO_NET_Library.Interfaces;
using ADONET.Models;
using Microsoft.Data.SqlClient;

namespace ADO_NET_Library.Repositories;

internal class ProductRepository
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

        using SqlCommand command = new (sql, connection);
        command.Parameters.AddWithValue("@Id", product.Id);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@Weight", product.Weight);
        command.Parameters.AddWithValue("@Height", product.Height);
        command.Parameters.AddWithValue("@Width", product.Width);
        command.Parameters.AddWithValue("@Length", product.Length);

        command.ExecuteNonQuery();
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

        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@Id", product.Id);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@Weight", product.Weight);
        command.Parameters.AddWithValue("@Height", product.Height);
        command.Parameters.AddWithValue("@Width", product.Width);
        command.Parameters.AddWithValue("@Length", product.Length);

        command.ExecuteNonQuery();
    }

    public void Delete(int productId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE dbo.product WHERE Id = @Id";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", productId);
        command.ExecuteNonQuery();
    }

    public List<Product> SelectAll()
    {
        List<Product> products = new ();

        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM dbo.product";

        using var command = new SqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var product = new Product
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Weight = reader.GetFloat(reader.GetOrdinal("Weight")), 
                Height = reader.GetFloat(reader.GetOrdinal("Height")), 
                Width = reader.GetFloat(reader.GetOrdinal("Width")),   
                Length = reader.GetFloat(reader.GetOrdinal("Length"))  
            };

            products.Add(product);
        }

        return products;
    }

    public Product? SelectById(int productId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM dbo.product WHERE Id = @Id";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", productId);
        using var reader = command.ExecuteReader();

        if (!reader.Read()) return null;
        var product = new Product
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Description = reader.GetString(reader.GetOrdinal("Description")),
            Weight = reader.GetFloat(reader.GetOrdinal("Weight")),
            Height = reader.GetFloat(reader.GetOrdinal("Height")),
            Width = reader.GetFloat(reader.GetOrdinal("Width")),
            Length = reader.GetFloat(reader.GetOrdinal("Length"))
        };

        return product;
    }
}
