using Web_API.Models;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace REST_Client_Tests;

[TestClass]
public class ProductApiTests
{
    private static HttpClient _client;
    private static JsonSerializerOptions jsonOptions;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };

        jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [TestMethod]
    public async Task TestGetProducts()
    {
        // Act
        var response = await _client.GetAsync("api/products");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<Product>>(content);

        // Assert
        Assert.IsNotNull(products);
        Assert.IsTrue(products.Count > 0);
    }

    [TestMethod]
    public async Task TestGetProductsWithPagination()
    {
        // Act
        var response = await _client.GetAsync("api/products?pageNumber=1&pageSize=5");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<Product>>(content);

        // Assert
        Assert.IsNotNull(products);
        Assert.IsTrue(products.Count <= 5);
    }

    [TestMethod]
    public async Task TestPostProduct()
    {
        // Arrange
        var product = CreateMockProduct();

        // Act
        var content = new StringContent(JsonSerializer.Serialize(product), System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("api/products", content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdProduct = JsonSerializer.Deserialize<Product>(responseContent, jsonOptions);

        // Assert
        Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        Assert.IsNotNull(createdProduct);
        Assert.IsTrue(createdProduct.ProductID > 0);
    }

    [TestMethod]
    public async Task TestPutProduct()
    {
        // Arrange
        var newProduct = CreateMockProduct();

        // Act
        var postContent = new StringContent(JsonSerializer.Serialize(newProduct), System.Text.Encoding.UTF8, "application/json");
        var postResponse = await _client.PostAsync("api/products", postContent);
        var postResponseContent = await postResponse.Content.ReadAsStringAsync();
        var createdProduct = JsonSerializer.Deserialize<Product>(postResponseContent, jsonOptions);
        
        createdProduct.ProductName = "Updated Product";

        var putContent = new StringContent(JsonSerializer.Serialize(createdProduct), System.Text.Encoding.UTF8, "application/json");
        var putResponse = await _client.PutAsync($"api/products/{createdProduct.ProductID}", putContent);

        // Assert
        putResponse.EnsureSuccessStatusCode();
    }

    [TestMethod]
    public async Task TestDeleteProduct()
    {
        // Arrange
        var newProduct = CreateMockProduct();

        // Act
        var postContent = new StringContent(JsonSerializer.Serialize(newProduct), System.Text.Encoding.UTF8, "application/json");
        var postResponse = await _client.PostAsync("api/products", postContent);
        var postResponseContent = await postResponse.Content.ReadAsStringAsync();
        var createdProduct = JsonSerializer.Deserialize<Product>(postResponseContent, jsonOptions);
        var deleteResponse = await _client.DeleteAsync($"api/products/{createdProduct.ProductID}");

        // Assert
        deleteResponse.EnsureSuccessStatusCode();
    }

    public Product CreateMockProduct()
    {
        var mockProduct = new Product
        {
            ProductID = 0, 
            ProductName = "Test Product",
            SupplierID = null, 
            CategoryID = null, 
            QuantityPerUnit = "12 per box",
            UnitPrice = 19.99m,
            UnitsInStock = 100,
            UnitsOnOrder = 10,
            ReorderLevel = 5,
            Discontinued = false
        };

        return mockProduct;
    }
}