using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Products;
using DemoWebApiDB.Tests.TestInfrastructure;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace DemoWebApiDB.Tests.ProductTests;


/// <summary>
///     Integration tests for Product GET endpoints.
///     
///     Demonstrates:
///     - DTO projection validation
///     - audit fields returned
///     - correct HTTP semantics
///     - deterministic ordering
/// </summary>
public sealed class Product_Get_Tests
{

    #region GET BY ID - SUCCESS

    [Fact]
    public async Task GetProductById_ShouldReturn200_WhenExists()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();

        // ----- Act

        var response 
            = await client.GetAsync( $"/api/products/{product.ProductId}", TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto 
            = await response.Content.ReadFromJsonAsync<ProductReadDTO>(TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto!.ProductId.Should().Be(product.ProductId);
        dto.ProductName.Should().Be(product.ProductName);
        dto.CategoryId.Should().Be(product.CtgryId);

        // audit + concurrency
        dto.CreatedAtUtc.Should().NotBe(default);
        dto.RowVersion.Should().NotBeNullOrWhiteSpace();
    }

    #endregion


    #region GET BY ID - NOT FOUND

    [Fact]
    public async Task GetProductById_ShouldReturn404_WhenNotFound()
    {
        // ----- Arrange
        
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        // ----- Act

        var response 
            = await client.GetAsync( $"/api/products/{Guid.NewGuid()}", TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion


    #region GET ALL - SUCCESS

    [Fact]
    public async Task GetAllProducts_ShouldReturn200_AndList()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        // ----- Act

        var response = await client.GetAsync( "/api/products", TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProductReadDTO>>(
            cancellationToken: TestContext.Current.CancellationToken);

        products.Should().NotBeNull();
        products.Should().NotBeEmpty();
    }

    #endregion


    #region GET ALL - ORDERED

    [Fact]
    public async Task GetAllProducts_ShouldBeOrderedByProductName()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        // ----- Act

        var response 
            = await client.GetAsync("/api/products", TestContext.Current.CancellationToken);

        // ----- Assert

        var products = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProductReadDTO>>(
            cancellationToken: TestContext.Current.CancellationToken);

        products!
            .Select(p => p.ProductName)
            .Should()
            .BeInAscendingOrder();
    }

    #endregion


    #region GET ALL - EMPTY

    [Fact]
    public async Task GetAllProducts_ShouldReturnEmptyList_WhenNoProductsExist()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        db.Products.RemoveRange(db.Products);
        db.SaveChanges();

        // ----- Act

        var response = await client.GetAsync(
            "/api/products",
            TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProductReadDTO>>(
            cancellationToken: TestContext.Current.CancellationToken);

        products.Should().NotBeNull();
        products.Should().BeEmpty();
    }

    #endregion

}