using System.Data;
using System.Reflection.PortableExecutable;
using ADO_NET_Library.Interfaces;
using ADO_NET_Library.Models;
using Microsoft.Data.SqlClient;

namespace ADO_NET_Library.Repositories;

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

        int updatedRecords = command.ExecuteNonQuery();

        if (updatedRecords == 0) 
            throw new ArgumentException($"Non existent ProductId {product.Id}");
    }

    public List<Product> SelectAll()
    {
        List<Product> products = new ();

        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM dbo.product";

        // Disconnected model realization

        using SqlDataAdapter adapter = new(sql, connection);
        var ds = new DataSet();
        adapter.Fill(ds, nameof(Product));

        var productTable = ds.Tables[nameof(Product)];
        if (productTable is null) return products;

        products.AddRange(from DataRow row in productTable.Rows
                          select new Product
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              Name = row["Name"].ToString() ?? string.Empty,
                              Description = row["Description"].ToString() ?? string.Empty,
                              Weight = Convert.ToSingle(row["Weight"]),
                              Height = Convert.ToSingle(row["Height"]),
                              Width = Convert.ToSingle(row["Width"]),
                              Length = Convert.ToSingle(row["Length"])
                          });

        // Connected model realization

        //using var command = new SqlCommand(sql, connection);
        //using var reader = command.ExecuteReader();

        //while (reader.Read())
        //{
        //    var product = new Product
        //    {
        //        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //        Name = reader.GetString(reader.GetOrdinal("Name")),
        //        Description = reader.GetString(reader.GetOrdinal("Description")),
        //        Weight = reader.GetFloat(reader.GetOrdinal("Weight")),
        //        Height = reader.GetFloat(reader.GetOrdinal("Height")),
        //        Width = reader.GetFloat(reader.GetOrdinal("Width")),
        //        Length = reader.GetFloat(reader.GetOrdinal("Length"))
        //    };

        //    products.Add(product);
        //}

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

    public void DeleteById(int productId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE dbo.product WHERE Id = @Id";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", productId);

        int updatedRecords = command.ExecuteNonQuery();

        if (updatedRecords == 0)
            throw new ArgumentException($"Non existent ProductId {productId}");
    }

    public void DeleteAll()
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE dbo.product";

        using var command = new SqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }
}
