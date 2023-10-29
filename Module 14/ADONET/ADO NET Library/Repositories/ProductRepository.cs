using ADONET.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ADONET.Repositories;

internal class ProductRepository
{
    private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ado_module_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public void Insert()
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = """
                     INSERT INTO dbo.product
                     (Name, Description, Weight, Height, Width, Length)
                     VALUES (
                        @Name,
                        @Description,
                        @Weight,
                        @Height,
                        @Width,
                        @Length
                     )
                     """;

        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Name", "Product1");
        command.Parameters.AddWithValue("@Description", "Description1");
        command.Parameters.AddWithValue("@Weight", 0);
        command.Parameters.AddWithValue("@Height", 0);
        command.Parameters.AddWithValue("@Width",0);
        command.Parameters.AddWithValue("@Length", 0);

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 0)
            throw new Exception("Something went wrong during insertion");

        //throw new NotImplementedException();
    }

    public void Update(int productId, Product product)
    {
        throw new NotImplementedException();
    }

    public void Delete(int orderId)
    {
        throw new NotImplementedException();
    }

    public List<Product> Select()
    {
        throw new NotImplementedException();
    }
}
