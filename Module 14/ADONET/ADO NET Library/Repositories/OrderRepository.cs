using System.Data;
using ADO_NET_Library.Enums;
using ADO_NET_Library.Interfaces;
using ADO_NET_Library.Models;
using Microsoft.Data.SqlClient;

namespace ADO_NET_Library.Repositories;

public class OrderRepository
{
    private readonly IDatabaseConnector _databaseConnector;

    public OrderRepository(IDatabaseConnector databaseConnector)
    {
        _databaseConnector = databaseConnector;
    }

    public void Insert(Order order)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = """
                           INSERT INTO [dbo].[Order]
                           (Id, Status, CreatedDate, UpdatedDate, ProductId)
                           VALUES (
                              @Id,
                              @Status,
                              @CreatedDate,
                              @UpdatedDate,
                              @ProductId
                           )
                           """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@Id", order.Id);
        command.Parameters.AddWithValue("@Status", order.Status.ToString());
        command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
        command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
        command.Parameters.AddWithValue("@ProductId", order.ProductId);

        command.ExecuteNonQuery();
    }

    public void Update(Order order)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = """
                           UPDATE [dbo].[order]
                           SET
                               Status = @Status,
                               CreatedDate = @CreatedDate,
                               UpdatedDate = @UpdatedDate,
                               ProductId = @ProductId
                           WHERE Id = @Id
                           """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@Id", order.Id);
        command.Parameters.AddWithValue("@Status", order.Status.ToString());
        command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
        command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
        command.Parameters.AddWithValue("@ProductId", order.ProductId);
        
        var updatedRecords = command.ExecuteNonQuery();

        if (updatedRecords == 0)
            throw new ArgumentException($"Non existent OrderId {order.Id}");
    }

    public List<Order> SelectAll()
    {
        List<Order> orders = new();

        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM [dbo].[order]";

        // Disconnected model realization

        using SqlDataAdapter adapter = new(sql, connection);
        var ds = new DataSet();
        adapter.Fill(ds, nameof(Order));

        var orderTable = ds.Tables[nameof(Order)];
        if (orderTable is null) return orders;

        orders.AddRange(from DataRow row in orderTable.Rows
            select new Order
            {
                Id = Convert.ToInt32(row["Id"]),
                Status = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), row["Status"].ToString()),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]),
                ProductId = Convert.ToInt32(row["ProductId"])
            });

        // Connected model realization

        //using var command = new SqlCommand(sql, connection);
        //using var reader = command.ExecuteReader();

        //while (reader.Read())
        //{
        //    var order = new Order
        //    {
        //        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //        Status = (EOrderStatus) Enum.Parse(typeof(EOrderStatus), reader.GetString(reader.GetOrdinal("Status"))),
        //        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
        //        UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
        //        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId"))
        //    };

        //    orders.Add(order);
        //}

        return orders;
    }

    public Order? SelectById(int orderId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM [dbo].[order] WHERE Id = @Id";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", orderId);
        using var reader = command.ExecuteReader();

        if (!reader.Read()) return null;
        var order = new Order
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Status = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), reader.GetString(reader.GetOrdinal("Status"))),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
            ProductId = reader.GetInt32(reader.GetOrdinal("ProductId"))
        };

        return order;
    }

    public List<Order> SelectByFilter(int? orderCreatedMonth, int? orderCreatedYear, 
        EOrderStatus? status, string? productName)
    {
        var orders = new List<Order>();

        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        var procedureName = EStoredProcedure.SelectOrdersByFilter.ToString();
        using var command = new SqlCommand(procedureName, connection);
        
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@orderCreatedMonth", orderCreatedMonth ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@orderCreatedYear", orderCreatedYear ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@status", status?.ToString() ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@productName", productName ?? (object)DBNull.Value);
        
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var order = new Order
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Status = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), reader.GetString(reader.GetOrdinal("Status"))),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId"))
            };

            orders.Add(order);
        }

        return orders;
    }

    public void DeleteById(int orderId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE [dbo].[order] WHERE Id = @Id";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", orderId);

        var updatedRecords = command.ExecuteNonQuery();

        if (updatedRecords == 0)
            throw new ArgumentException($"Non existent OrderId {orderId}");
    }

    public void DeleteAll()
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE [dbo].[order]";

        using var command = new SqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    public void DeleteByFilter(int? orderCreatedMonth, int? orderCreatedYear,
        EOrderStatus? status, string? productName)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        var transaction = connection.BeginTransaction();

        try
        {
            var procedureName = EStoredProcedure.DeleteOrdersByFilter.ToString();
            using var command = new SqlCommand(procedureName, connection, transaction);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@orderCreatedMonth", orderCreatedMonth ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@orderCreatedYear", orderCreatedYear ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@status", status?.ToString() ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@productName", productName ?? (object)DBNull.Value);

            command.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }
}
