using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Products;
using DemoWebApiDB.Tests.TestInfrastructure;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;


namespace DemoWebApiDB.Tests.ProductTests;


/// <summary>
///     Integration tests for Product CREATE endpoint.
///     
///     Demonstrates:
///     - FK validation
///     - uniqueness within category
///     - business rule enforcement
///     - HTTP 201 semantics
/// </summary>
public sealed class Product_Create_Tests
{

    [Fact]
    public async Task CreateProduct_ShouldReturn201_WhenValid()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories.First();

        var dto = new ProductCreateDto(
            ProductName: "New Product",
            Price: 100,
            QtyInStock: 5,
            CategoryId: category.CategoryId
        );

        // ----- Act

        var response 
            = await client.PostAsJsonAsync("/api/products", dto, TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        response.Headers.Location.Should().NotBeNull();

        // verify if data updated in database
        using var verifyScope = factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var exists = verifyDb.Products
            .Any(p => p.ProductName == "New Product"
                   && p.CtgryId == category.CategoryId);

        exists.Should().BeTrue();
    }



    [Fact]
    public async Task CreateProduct_ShouldReturn409_WhenDuplicateNameInSameCategory()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var existingProduct = db.Products.First();

        // ----- Act

        var dto = new ProductCreateDto(
            ProductName: existingProduct.ProductName,
            Price: 50,
            QtyInStock: 1,
            CategoryId: existingProduct.CtgryId
        );

        var response 
            = await client.PostAsJsonAsync( "/api/products", dto, TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }


    [Fact]
    public async Task CreateProduct_ShouldAllowSameName_InDifferentCategory()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var existingProduct = db.Products.First();

        var otherCategory = db.Categories
            .First(c => c.CategoryId != existingProduct.CtgryId);

        // ----- Act

        var dto = new ProductCreateDto(
            ProductName: existingProduct.ProductName,
            Price: 100,
            QtyInStock: 10,
            CategoryId: otherCategory.CategoryId
        );

        var response 
            = await client.PostAsJsonAsync( "/api/products", dto, TestContext.Current.CancellationToken );

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }


    [Fact]
    public async Task CreateProduct_ShouldReturn404_WhenCategoryDoesNotExist()
    {
        // ----- Assert
        
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        // ----- Act

        var dto = new ProductCreateDto(
            ProductName: "Invalid FK",
            Price: 10,
            QtyInStock: 1,
            CategoryId: 999999
        );

        var response 
            = await client.PostAsJsonAsync( "/api/products", dto, TestContext.Current.CancellationToken );

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task CreateProduct_ShouldReturn400_WhenPriceIsNegative()
    {
        // ----- Arrange
        
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories.First();

        // ----- Act
        
        var dto = new ProductCreateDto(
            ProductName: "Invalid Product",
            Price: -10,
            QtyInStock: 1,
            CategoryId: category.CategoryId
        );

        var response 
            = await client.PostAsJsonAsync( "/api/products", dto, TestContext.Current.CancellationToken );

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

}
