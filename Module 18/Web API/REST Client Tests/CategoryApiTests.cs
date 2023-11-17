using Web_API.Models;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace REST_Client_Tests;

[TestClass]
public class CategoryApiTests
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
    public async Task TestGetCategories()
    {
        // Act
        var response = await _client.GetAsync("api/categories");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<List<Category>>(content, jsonOptions);

        // Assert
        Assert.IsNotNull(categories);
        Assert.IsTrue(categories.Count > 0);
    }

    [TestMethod]
    public async Task TestPostCategory()
    {
        // Arrange
        var category = CreateMockCategory();

        // Act
        var content = new StringContent(JsonSerializer.Serialize(category), System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("api/categories", content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonSerializer.Deserialize<Category>(responseContent, jsonOptions);

        // Assert
        Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        Assert.IsNotNull(createdCategory);
        Assert.IsTrue(createdCategory.CategoryID > 0);
    }

    [TestMethod]
    public async Task TestPutCategory()
    {
        // Arrange
        var newCategory = CreateMockCategory();

        // Act
        var postContent = new StringContent(JsonSerializer.Serialize(newCategory), System.Text.Encoding.UTF8, "application/json");
        var postResponse = await _client.PostAsync("api/categories", postContent);
        var postResponseContent = await postResponse.Content.ReadAsStringAsync();
        var createdCategory = JsonSerializer.Deserialize<Category>(postResponseContent, jsonOptions);

        createdCategory.CategoryName = "Updated name";

        var putContent = new StringContent(JsonSerializer.Serialize(createdCategory), System.Text.Encoding.UTF8, "application/json");
        var putResponse = await _client.PutAsync($"api/categories/{createdCategory.CategoryID}", putContent);

        // Assert
        putResponse.EnsureSuccessStatusCode();
    }

    [TestMethod]
    public async Task TestDeleteCategory()
    {
        // Arrange
        var newCategory = CreateMockCategory();

        // Act
        var postContent = new StringContent(JsonSerializer.Serialize(newCategory), System.Text.Encoding.UTF8, "application/json");
        var postResponse = await _client.PostAsync("api/categories", postContent);
        var postResponseContent = await postResponse.Content.ReadAsStringAsync();
        var createdCategory = JsonSerializer.Deserialize<Category>(postResponseContent, jsonOptions);

        var deleteResponse = await _client.DeleteAsync($"api/categories/{createdCategory.CategoryID}");

        // Assert
        deleteResponse.EnsureSuccessStatusCode();
    }

    private Category CreateMockCategory()
    {
        return new Category
        {
            CategoryID = 0,
            CategoryName = "Test Category",
            Description = "Test category description"
        };
    }
}