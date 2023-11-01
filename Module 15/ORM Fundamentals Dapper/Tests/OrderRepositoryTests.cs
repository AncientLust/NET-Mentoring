using FluentAssertions;
using Library.Database_connections;
using Library.Enums;
using Library.Repositories;
using Xunit;

namespace Tests;

[Collection("Repository tests")]
public class OrderRepositoryTests
{
    // Create operations


    [Fact]
    public void OneOrderMustBeInserted()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[0];

        // Act
        productRepository.Insert(product);
        orderRepository.Insert(order);
        var productCount = orderRepository.SelectAll().Count;

        // Assert
        productCount.Should().Be(1);
    }

    [Fact]
    public void InsertionWithoutValidProductIdMustThrowException()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[1];

        // Act
        productRepository.Insert(product);
        var productCount = () => orderRepository.Insert(order); ;

        // Assert
        productCount.Should().Throw<Exception>();
    }

    [Fact]
    public void OrderDuplicateInsertionMustThrowException()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[0];

        // Act
        productRepository.Insert(product);
        orderRepository.Insert(order);
        var insertOrderDuplicate = () => orderRepository.Insert(order);

        // Assert (Exception type depends on used DB connection)
        insertOrderDuplicate.Should().Throw<Exception>();
    }


    // Read operations


    [Fact]
    public void InsertedOrderMustBeSelected()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[0];

        // Act
        productRepository.Insert(product);
        orderRepository.Insert(order);
        var orders = orderRepository.SelectAll();

        // Assert
        orders.First().Should().BeEquivalentTo(order);
    }

    [Fact]
    public void InsertedOrderMustBeSelectedById()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[0];

        // Act
        productRepository.Insert(product);
        orderRepository.Insert(order);
        var orderById = orderRepository.SelectById(order.Id);

        // Assert
        orderById.Should().BeEquivalentTo(order);
    }

    [Fact]
    public void SelectByInvalidIdMustReturnNull()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();

        // Act
        var product = orderRepository.SelectById(1);

        // Assert
        product.Should().Be(null);
    }


    // Update operations


    [Fact]
    public void OrderStatusMustBeUpdated()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[0];
        var updatedStatus = EOrderStatus.Done;

        // Act
        productRepository.Insert(product);
        orderRepository.Insert(order);
        order.Status = updatedStatus;
        orderRepository.Update(order);
        var updatedOrder = orderRepository.SelectAll().First();

        // Assert
        updatedOrder.Status.Should().Be(updatedStatus);
    }

    [Fact]
    public void OrderCreatedDateMustBeUpdated()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[0];
        var updatedCreatedDate = order.CreatedDate + TimeSpan.FromDays(1);

        // Act
        productRepository.Insert(product);
        orderRepository.Insert(order);
        order.CreatedDate = updatedCreatedDate;
        orderRepository.Update(order);
        var updatedOrder = orderRepository.SelectAll().First();

        // Assert
        updatedOrder.CreatedDate.Should().Be(updatedCreatedDate);
    }

    [Fact]
    public void OrderUpdatedDateMustBeUpdated()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[0];
        var updatedUpdatedDate = order.UpdatedDate + TimeSpan.FromDays(1);

        // Act
        productRepository.Insert(product);
        orderRepository.Insert(order);
        order.UpdatedDate = updatedUpdatedDate;
        orderRepository.Update(order);
        var updatedOrder = orderRepository.SelectAll().First();

        // Assert
        updatedOrder.UpdatedDate.Should().Be(updatedUpdatedDate);
    }

    [Fact]
    public void OrderProductIdMustBeUpdated()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order = DataSource.Orders[0];
        order.ProductId = product1.Id;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order);
        order.ProductId = product2.Id;
        orderRepository.Update(order);
        var updatedOrder = orderRepository.SelectAll().First();

        // Assert
        updatedOrder.ProductId.Should().Be(product2.Id);
    }

    [Fact]
    public void UpdateOfNonExistentOrderMustThrowException()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var order = DataSource.Orders[0];

        // Act
        var updateNonExistentOrder = () => orderRepository.Update(order);

        // Assert
        updateNonExistentOrder.Should().Throw<ArgumentException>();
    }


    // Delete operations


    [Fact]
    public void AllOrdersMustBeDeleted()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        orderRepository.DeleteAll();
        var orders = orderRepository.SelectAll();

        // Assert
        orders.Count.Should().Be(0);
    }

    [Fact]
    public void OrderMustBeDeletedById()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product = DataSource.Products[0];
        var order = DataSource.Orders[0];

        // Act
        productRepository.Insert(product);
        orderRepository.Insert(order);
        orderRepository.DeleteById(order.Id);
        var orders = orderRepository.SelectAll();

        // Assert
        orders.Count.Should().Be(0);
    }

    [Fact]
    public void DeletionByInvalidOrderIdMustThrowException()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var order = DataSource.Orders[0];

        // Act
        var deletionByInvalidId = () => orderRepository.DeleteById(order.Id);

        // Assert
        deletionByInvalidId.Should().Throw<ArgumentException>();
    }


    // Stored procedures


    [Fact]
    public void SelectByFilterShouldReturnOrdersFilteredByOrderCreatedMonth()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];
        var createdMonthFilter = order1.CreatedDate.Month;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        var orders = orderRepository.SelectByFilter(createdMonthFilter, null, null, null);

        // Assert
        orders.Count.Should().Be(1);
        orders.First().Should().BeEquivalentTo(order1);
    }

    [Fact]
    public void SelectByFilterShouldReturnOrdersFilteredByOrderCreatedYear()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];
        var createdYearFilter = order1.CreatedDate.Year;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        var orders = orderRepository.SelectByFilter(null, createdYearFilter, null, null);

        // Assert
        orders.Count.Should().Be(1);
        orders.First().Should().BeEquivalentTo(order1);
    }

    [Fact]
    public void SelectByFilterShouldReturnOrdersFilteredByStatus()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];
        var statusFilter = order1.Status;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        var orders = orderRepository.SelectByFilter(null, null, statusFilter, null);

        // Assert
        orders.Count.Should().Be(1);
        orders.First().Should().BeEquivalentTo(order1);
    }

    [Fact]
    public void SelectByFilterShouldReturnOrdersFilteredByProductName()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];
        var productNameFilter = product1.Name;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        var orders = orderRepository.SelectByFilter(null, null, null, productNameFilter);

        // Assert
        orders.Count.Should().Be(1);
        orders.First().Should().BeEquivalentTo(order1);
    }

    [Fact]
    public void DeleteByFilterShouldDeleteOrdersFilteredByOrderCreatedMonth()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];
        var createdMonthFilter = order1.CreatedDate.Month;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        orderRepository.DeleteByFilter(createdMonthFilter, null, null, null);
        var orders = orderRepository.SelectAll();

        // Assert
        orders.Count.Should().Be(1);
        orders.First().Should().BeEquivalentTo(order2);
    }

    [Fact]
    public void DeleteByFilterShouldDeleteOrdersFilteredByOrderCreatedYear()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];
        var createdYearFilter = order1.CreatedDate.Year;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        orderRepository.DeleteByFilter(null, createdYearFilter, null, null);
        var orders = orderRepository.SelectAll();

        // Assert
        orders.Count.Should().Be(1);
        orders.First().Should().BeEquivalentTo(order2);
    }

    [Fact]
    public void DeleteByFilterShouldDeleteOrdersFilteredByStatus()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];
        var statusFilter = order1.Status;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        orderRepository.DeleteByFilter(null, null, statusFilter, null);
        var orders = orderRepository.SelectAll();

        // Assert
        orders.Count.Should().Be(1);
        orders.First().Should().BeEquivalentTo(order2);
    }

    [Fact]
    public void DeleteByFilterShouldDeleteOrdersFilteredByProductName()
    {
        // Arrange 
        var orderRepository = GetCleanOrderRepository();
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];
        var order1 = DataSource.Orders[0];
        var order2 = DataSource.Orders[1];
        var productNameFilter = product1.Name;

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        orderRepository.Insert(order1);
        orderRepository.Insert(order2);
        orderRepository.DeleteByFilter(null, null, null, productNameFilter);
        var orders = orderRepository.SelectAll();

        // Assert
        orders.Count.Should().Be(1);
        orders.First().Should().BeEquivalentTo(order2);
    }

    private OrderRepository GetCleanOrderRepository()
    {
        var databaseConnector = new SqlServerConnector();
        var orderRepository = new OrderRepository(databaseConnector);
        orderRepository.DeleteAll();
        return orderRepository;
    }

    private ProductRepository GetCleanProductRepository()
    {
        var databaseConnector = new SqlServerConnector();
        var productRepository = new ProductRepository(databaseConnector);
        productRepository.DeleteAll();
        return productRepository;
    }
}