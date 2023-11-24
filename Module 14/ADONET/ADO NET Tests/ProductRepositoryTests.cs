using ADO_NET_Library.Database_connections;
using ADO_NET_Library.Repositories;
using FluentAssertions;
using Xunit;

namespace ADO_NET_Tests;

[Collection("Repository tests")]
public class ProductRepositoryTests
{
    
    // Create operations
    
    [Fact]
    public void OneProductMustBeInserted()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];

        // Act
        productRepository.Insert(product1);
        var productCount = productRepository.SelectAll().Count;

        // Assert
        productCount.Should().Be(1);
    }

    [Fact]
    public void ProductDuplicateInsertionMustThrowException()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];

        // Act
        productRepository.Insert(product1);
        var insertProductDuplicate = () => productRepository.Insert(product1);

        // Assert (Exception type depends on used DB connection)
        insertProductDuplicate.Should().Throw<Exception>();
    }

    
    // Read operations
    

    [Fact]
    public void InsertedProductMustBeSelected()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];

        // Act
        productRepository.Insert(product1);
        var products = productRepository.SelectAll();

        // Assert
        products.First().Should().BeEquivalentTo(product1);
    }

    [Fact]
    public void InsertedProductMustBeSelectedById()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        var product = productRepository.SelectById(product1.Id);

        // Assert
        product.Should().BeEquivalentTo(product1);
    }

    [Fact]
    public void SelectByInvalidIdMustReturnNull()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();

        // Act
        var product = productRepository.SelectById(1);

        // Assert
        product.Should().Be(null);
    }

    
    // Update operations
    

    [Fact]
    public void ProductNameMustBeUpdated()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var updatedName = "Updated product name";

        // Act
        productRepository.Insert(product1);
        product1.Name = updatedName;
        productRepository.Update(product1);
        var updatedProduct = productRepository.SelectAll().First();

        // Assert
        updatedProduct.Name.Should().Be(updatedName);
    }

    [Fact]
    public void ProductDescriptionMustBeUpdated()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var updatedDescription = "Updated product description";

        // Act
        productRepository.Insert(product1);
        product1.Description = updatedDescription;
        productRepository.Update(product1);
        var updatedProduct = productRepository.SelectAll().First();

        // Assert
        updatedProduct.Description.Should().Be(updatedDescription);
    }

    [Fact]
    public void ProductWeightMustBeUpdated()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var updatedWeight = product1.Weight + 1;

        // Act
        productRepository.Insert(product1);
        product1.Weight = updatedWeight;
        productRepository.Update(product1);
        var updatedProduct = productRepository.SelectAll().First();

        // Assert
        updatedProduct.Weight.Should().Be(updatedWeight);
    }

    [Fact]
    public void ProductHeightMustBeUpdated()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var updatedHeight = product1.Height + 1;

        // Act
        productRepository.Insert(product1);
        product1.Height = updatedHeight;
        productRepository.Update(product1);
        var updatedProduct = productRepository.SelectAll().First();

        // Assert
        updatedProduct.Height.Should().Be(updatedHeight);
    }

    [Fact]
    public void ProductWidthMustBeUpdated()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var updatedWidth = product1.Width + 1;

        // Act
        productRepository.Insert(product1);
        product1.Width = updatedWidth;
        productRepository.Update(product1);
        var updatedProduct = productRepository.SelectAll().First();

        // Assert
        updatedProduct.Width.Should().Be(updatedWidth);
    }

    [Fact]
    public void ProductLengthMustBeUpdated()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var updatedLength = product1.Length + 1;

        // Act
        productRepository.Insert(product1);
        product1.Length = updatedLength;
        productRepository.Update(product1);
        var updatedProduct = productRepository.SelectAll().First();

        // Assert
        updatedProduct.Length.Should().Be(updatedLength);
    }

    [Fact]
    public void UpdateOfNonExistentProductMustThrowException()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];

        // Act
        var updateNonExistentProduct = () => productRepository.Update(product1);

        // Assert
        updateNonExistentProduct.Should().Throw<ArgumentException>();
    }

    
    // Delete operations
    

    [Fact]
    public void AllProductMustBeDeleted()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        productRepository.DeleteAll();

        var products = productRepository.SelectAll();

        // Assert
        products.Count.Should().Be(0);
    }

    [Fact]
    public void ProductMustBeDeletedById()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];
        var product2 = DataSource.Products[1];

        // Act
        productRepository.Insert(product1);
        productRepository.Insert(product2);
        productRepository.DeleteById(product1.Id);
        var products = productRepository.SelectAll();

        // Assert
        products.Count.Should().Be(1);
        products[0].Should().BeEquivalentTo(product2);
    }

    [Fact]
    public void DeletionByInvalidProductIdMustThrowException()
    {
        // Arrange 
        var productRepository = GetCleanProductRepository();
        var product1 = DataSource.Products[0];

        // Act
        var deletionByInvalidId = () => productRepository.DeleteById(product1.Id);

        // Assert
        deletionByInvalidId.Should().Throw<ArgumentException>();
    }

    private ProductRepository GetCleanProductRepository()
    {
        var databaseConnector = new SqlServerConnector();
        var productRepository = new ProductRepository(databaseConnector);
        var orderRepository = new OrderRepository(databaseConnector);
        orderRepository.DeleteAll();
        productRepository.DeleteAll();
        
        return productRepository;
    }
}