using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Products;
using DemoWebApiDB.DtoModels.ReadModels;
using DemoWebApiDB.Tests.TestInfrastructure;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace DemoWebApiDB.Tests.ProductTests;


/// <summary>
///     Integration tests for Products-With-Category VIEW endpoint.
///
///     Demonstrates:
///     - EF View mapping
///     - Keyless read model projection
///     - Reporting endpoint validation
///     - Deterministic ordering
/// </summary>
public sealed class Product_View_Tests
{
    #region SUCCESS

    [Fact]
    public async Task GetProductsWithCategory_ShouldReturn200_AndData()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        // ----- Act

        var response 
            = await client.GetAsync( "/api/products/with-category", TestContext.Current.CancellationToken);

        // ----- Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProductWithCategoryReadModel>>(
            cancellationToken: TestContext.Current.CancellationToken);

        data.Should().NotBeNull();
        data.Should().NotBeEmpty();
    }

    #endregion


    #region VALIDATE PROJECTION

    [Fact]
    public async Task GetProductsWithCategory_ShouldReturnCorrectProjection()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var expected = db.Products.First();
        var expectedCategory = db.Categories.First(c => c.CategoryId == expected.CtgryId);

        // ----- Act
        
        var response 
            = await client.GetAsync("/api/products/with-category", TestContext.Current.CancellationToken);

        // var data2 = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProductWithCategoryReadModel>>(
        //    cancellationToken: TestContext.Current.CancellationToken);

        var data = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProductWithCategoryDto>>(
            cancellationToken: TestContext.Current.CancellationToken);

        // ----- Assert

        var result = data!.First(p => p.ProductId == expected.ProductId);

        result.ProductName.Should().Be(expected.ProductName);
        result.CategoryId.Should().Be(expectedCategory.CategoryId);
        result.CategoryName.Should().Be(expectedCategory.Name);
    }

    #endregion


    #region ORDERED

    [Fact]
    public async Task GetProductsWithCategory_ShouldBeOrderedByProductName()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        // ----- Act

        var response = await client.GetAsync("/api/products/with-category", TestContext.Current.CancellationToken);

        // ----- Assert

        var data = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProductWithCategoryReadModel>>(
            cancellationToken: TestContext.Current.CancellationToken);

        data!
            .Select(p => p.ProductName)
            .Should()
            .BeInAscendingOrder();
    }

    #endregion


    #region EMPTY RESULT

    [Fact]
    public async Task GetProductsWithCategory_ShouldReturnEmpty_WhenNoProductsExist()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        db.Products.RemoveRange(db.Products);
        db.SaveChanges();

        // ----- Act

        var response = await client.GetAsync( "/api/products/with-category", TestContext.Current.CancellationToken);

        // ----- Assert
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProductWithCategoryReadModel>>(
            cancellationToken: TestContext.Current.CancellationToken);

        data.Should().NotBeNull();
        data.Should().BeEmpty();
    }

    #endregion

}