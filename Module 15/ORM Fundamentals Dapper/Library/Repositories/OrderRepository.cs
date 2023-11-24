using System.Data;
using Library.Enums;
using Library.Interfaces;
using Library.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Library.Repositories;

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

        // Enum requires case to string
        var orderToInsert = new
        {
            Id = order.Id,
            Status = order.Status.ToString(),
            CreatedDate = order.CreatedDate,
            UpdatedDate = order.UpdatedDate,
            ProductId = order.ProductId
        };

        connection.Execute(sql, orderToInsert);
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

        // Enum requires case to string
        var orderToInsert = new
        {
            Id = order.Id,
            Status = order.Status.ToString(),
            CreatedDate = order.CreatedDate,
            UpdatedDate = order.UpdatedDate,
            ProductId = order.ProductId
        };

        var updatedRecords = connection.Execute(sql, orderToInsert);

        if (updatedRecords == 0)
            throw new ArgumentException($"Non existent OrderId {order.Id}");
    }

    public List<Order> SelectAll()
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM [dbo].[order]";

        return connection.Query<Order>(sql).ToList();
    }

    public Order? SelectById(int orderId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "SELECT * FROM [dbo].[order] WHERE Id = @Id";

        return connection.QueryFirstOrDefault<Order>(sql, new {Id = orderId });
    }

    public List<Order> SelectByFilter(int? orderCreatedMonth, int? orderCreatedYear, 
        EOrderStatus? status, string? productName)
    {

        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        var procedureName = EStoredProcedure.SelectOrdersByFilter.ToString();

        var parameters = new DynamicParameters();
        parameters.Add("@orderCreatedMonth", orderCreatedMonth, DbType.Int32, ParameterDirection.Input);
        parameters.Add("@orderCreatedYear", orderCreatedYear, DbType.Int32, ParameterDirection.Input);
        parameters.Add("@status", status?.ToString(), DbType.String, ParameterDirection.Input);
        parameters.Add("@productName", productName, DbType.String, ParameterDirection.Input);
        
        var orders = connection.Query<Order>(
            procedureName, 
            parameters, 
            commandType: 
            CommandType.StoredProcedure).ToList();

        return orders;
    }

    public void DeleteById(int orderId)
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE [dbo].[order] WHERE Id = @Id";

        var updatedRecords = connection.Execute(sql, new { Id = orderId });

        if (updatedRecords == 0)
            throw new ArgumentException($"Non existent OrderId {orderId}");
    }

    public void DeleteAll()
    {
        using var connector = _databaseConnector;
        var connection = _databaseConnector.OpenConnection();

        const string sql = "DELETE [dbo].[order]";

        connection.Execute(sql);
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

            var parameters = new DynamicParameters();
            parameters.Add("@orderCreatedMonth", orderCreatedMonth, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@orderCreatedYear", orderCreatedYear, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@status", status?.ToString(), DbType.String, ParameterDirection.Input);
            parameters.Add("@productName", productName, DbType.String, ParameterDirection.Input);

            connection.Query<Order>(procedureName, parameters, transaction);

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
