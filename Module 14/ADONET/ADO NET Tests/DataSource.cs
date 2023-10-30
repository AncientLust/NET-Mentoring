using ADO_NET_Library.Enums;
using ADO_NET_Library.Models;

namespace ADO_NET_Tests;

internal class DataSource
{
    public static List<Product> Products =>
        new()
        {
            new Product()
            {
                Id = 1,
                Name = "Name1",
                Description = "Description1",
                Weight = 1.1f,
                Height = 1.1f,
                Width = 1.1f,
                Length = 1.1f
            },

            new Product()
            {
                Id = 2,
                Name = "Name2",
                Description = "Description2",
                Weight = 2.2f,
                Height = 2.2f,
                Width = 2.2f,
                Length = 2.2f
            }
        };

    public static List<Order> Orders =>
        new()
        {
            new Order()
            {
                Id = 1,
                Status = EOrderStatus.NotStarted,
                CreatedDate = new DateTime(2001, 1, 1, 1, 1, 1),
                UpdatedDate = new DateTime(2001, 1, 1, 1, 1, 1),
                ProductId = 1
            },

            new Order()
            {
                Id = 2,
                Status = EOrderStatus.InProgress,
                CreatedDate = new DateTime(2002, 2, 2, 2, 2, 2),
                UpdatedDate = new DateTime(2002, 2, 2, 2, 2, 2),
                ProductId = 2
            }
        };
}
