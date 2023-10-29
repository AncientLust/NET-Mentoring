using ADO_NET_Library.Database_connections;
using ADO_NET_Library.Enums;
using ADO_NET_Library.Repositories;
using ADONET.Models;

namespace ADO_NET_Library
{
    public static class Program
    {
        public static void Main()
        {
            var sqlConnector = new SqlServerConnector();
            var productRepository = new ProductRepository(sqlConnector);
            var orderRepository = new OrderRepository(sqlConnector);

            var product1 = new Product()
            {
                Id = 1,
                Name = "Product1",
                Description = "Description1",
                Weight = 1,
                Height = 1,
                Width = 1,
                Length = 1
            };

            var product2 = new Product()
            {
                Id = 2,
                Name = "Product2",
                Description = "Description2",
                Weight = 2,
                Height = 2,
                Width = 2,
                Length = 2
            };

            var order1 = new Order()
            {
                Id = 1,
                Status = EOrderStatus.NotStarted,
                CreatedDate = new DateTime(2001, 1, 1, 1, 1, 1),
                UpdatedDate = new DateTime(2001, 1, 1, 1, 1, 1),
                ProductId = 1
            };

            var order2 = new Order()
            {
                Id = 2,
                Status = EOrderStatus.InProgress,
                CreatedDate = new DateTime(2002, 2, 2, 2, 2, 2),
                UpdatedDate = new DateTime(2002, 2, 2, 2, 2, 2),
                ProductId = 2
            };

            //productRepository.Insert(product1);
            //productRepository.Insert(product2);
            //productRepository.Update(product);
            //productRepository.Delete(product1);
            //var products = productRepository.SelectAll();
            //var products = productRepository.SelectById(2);

            //orderRepository.Insert(order1);
            //orderRepository.Insert(order2);
            //orderRepository.Update(order2);
            //orderRepository.Delete(order2.Id);
            //var orders = orderRepository.SelectAll();
            //var orders = orderRepository.SelectById(2);
            var orders = orderRepository.SelectByFilter(null, null, null, "Product2");

            //Console.WriteLine(orders.Count);
        }
    }
}