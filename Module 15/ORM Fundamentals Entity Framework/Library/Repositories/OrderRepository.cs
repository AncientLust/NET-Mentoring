using Library.Enums;
using Library.Models;
using Library.Database_connections;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class OrderRepository
{
    public void Insert(Order order)
    {
        using var context = new SqlServerContext();
        context.Orders.Add(order);
        context.SaveChanges();
    }

    public void Update(Order order)
    {
        using var context = new SqlServerContext();

        if (!context.Orders.Contains(order))
            throw new ArgumentException($"Non existent OrderId {order.Id}");

        context.Orders.Update(order);
        context.SaveChanges();
    }

    public List<Order> SelectAll()
    {
        using var context = new SqlServerContext();
        return context.Orders.ToList();
    }

    public Order? SelectById(int orderId)
    {
        using var context = new SqlServerContext();
        return context.Orders.Find(orderId);
    }

    public List<Order> SelectByFilter(int? orderCreatedMonth, int? orderCreatedYear, 
        EOrderStatus? status, string? productName)
    {
        using var context = new SqlServerContext();
        
        var orders = context.Orders.FromSqlInterpolated($"""
             EXEC {EStoredProcedure.SelectOrdersByFilter.ToString()}
                @orderCreatedMonth = {orderCreatedMonth},
                @orderCreatedYear = {orderCreatedYear},
                @status = {status?.ToString()},
                @productName = {productName}
             """).ToList();
        
        return orders;
    }

    public void DeleteById(int orderId)
    {
        using var context = new SqlServerContext();
        var order = context.Orders.Find(orderId);

        if (order is null)
            throw new ArgumentException($"Non existent OrderId {orderId}");

        context.Orders.Remove(order);
        context.SaveChanges();
    }

    public void DeleteAll()
    {
        using var context = new SqlServerContext();
        var orders = context.Orders.ToList();
        context.Orders.RemoveRange(orders);
        context.SaveChanges();
    }

    public void DeleteByFilter(int? orderCreatedMonth, int? orderCreatedYear,
        EOrderStatus? status, string? productName)
    {
        using var context = new SqlServerContext();
        using var transaction = context.Database.BeginTransaction();

        try
        {
            context.Database.ExecuteSqlInterpolated($"""
                EXEC {EStoredProcedure.DeleteOrdersByFilter.ToString()}
                   @orderCreatedMonth = {orderCreatedMonth},
                   @orderCreatedYear = {orderCreatedYear},
                   @status = {status?.ToString()},
                   @productName = {productName}
                """);

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
