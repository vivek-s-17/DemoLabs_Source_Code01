using System.Net;
using System.Net.Http.Json;

using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Products;
using DemoWebApiDB.Tests.TestInfrastructure;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace DemoWebApiDB.Tests.ProductTests;


/// <summary>
///     Integration tests for Product UPDATE endpoint.
///     
///     Demonstrates:
///     - Success update
///     - Route/body mismatch
///     - Not found
///     - Duplicate name within same category
///     - Move to different category
///     - Concurrency conflict
///     - Invalid category
///     - Invalid payload(negative price)
/// </summary>
public sealed class Product_Update_Tests
{

    [Fact]
    public async Task UpdateProduct_ShouldReturn202_WhenValid()
    {
        // ----- Arrange
        
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();

        // ----- Act

        var dto = new ProductUpdateDto(
                    ProductId: product.ProductId,
                    ProductName: "Updated Product",
                    Price: 200,
                    QtyInStock: 20,
                    CategoryId: product.CtgryId,
                    RowVersion: Convert.ToBase64String(product.RowVersion)
                );
        
        var response 
            = await client.PutAsJsonAsync( $"/api/products/{product.ProductId}", dto, TestContext.Current.CancellationToken);

        // ---- Assert

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        using var verifyScope = factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var updated = verifyDb.Products.First(p => p.ProductId == product.ProductId);

        updated.ProductName.Should().Be("Updated Product");
        updated.Price.Should().Be(200);
    }


    [Fact]
    public async Task UpdateProduct_ShouldReturn400_WhenRouteBodyMismatch()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();

        // ----- Arrange

        var dto = new ProductUpdateDto(
            ProductId: Guid.NewGuid(), // mismatch
            ProductName: product.ProductName,
            Price: product.Price,
            QtyInStock: product.QtyInStock,
            CategoryId: product.CtgryId,
            RowVersion: Convert.ToBase64String(product.RowVersion)
        );

        var response 
            = await client.PutAsJsonAsync( $"/api/products/{product.ProductId}", dto, TestContext.Current.CancellationToken);

        // ----- Act

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task UpdateProduct_ShouldReturn404_WhenNotFound()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        // ----- Act

        var dto = new ProductUpdateDto(
            ProductId: Guid.NewGuid(),
            ProductName: "Ghost",
            Price: 10,
            QtyInStock: 1,
            CategoryId: 1,
            RowVersion: Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        );

        var response 
            = await client.PutAsJsonAsync( $"/api/products/{dto.ProductId}", dto, TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task UpdateProduct_ShouldReturn409_WhenDuplicateNameInSameCategory()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // take two products from the seeded database - each from same category
        var group = db.Products
            .AsEnumerable()                     // ensure in-memory grouping
            .GroupBy(p => p.CtgryId)
            .First(g => g.Count() >= 2);

        var first = group.ElementAt(0);
        var second = group.ElementAt(1);

        /********
        // take a product from the seeded database - each from different categories
        var products = db.Products
            .GroupBy(p => p.CtgryId)            // Group by the category ID
            .Select(g => g.FirstOrDefault())    // Pick the first product in each category
            .Take(2)                            // Limit to only 2 products total
            .ToList();
        var first = products[0];
        var second = products[1];
        ******/

        // ----- Act

        var dto = new ProductUpdateDto(
            ProductId: first.ProductId,
            ProductName: second.ProductName,
            Price: first.Price,
            QtyInStock: first.QtyInStock,
            CategoryId: first.CtgryId,
            RowVersion: Convert.ToBase64String(first.RowVersion)
        );

        var response 
            = await client.PutAsJsonAsync( $"/api/products/{first.ProductId}", dto, TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }


    [Fact]
    public async Task UpdateProduct_ShouldAllowMovingToDifferentCategory()
    {
        // ---- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();
        var otherCategory = db.Categories.First(c => c.CategoryId != product.CtgryId);

        // ----- Act

        var dto = new ProductUpdateDto(
            ProductId: product.ProductId,
            ProductName: product.ProductName,
            Price: product.Price,
            QtyInStock: product.QtyInStock,
            CategoryId: otherCategory.CategoryId,
            RowVersion: Convert.ToBase64String(product.RowVersion)
        );

        var response = await client.PutAsJsonAsync(
            $"/api/products/{product.ProductId}",
            dto,
            TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturn409_WhenConcurrencyConflict()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();
        var oldRowVersion = Convert.ToBase64String(product.RowVersion);

        // ----- Act - Simulate another update
        product.ProductName = "Changed Elsewhere";
        db.SaveChanges();

        // ----- Act - call API to update

        var dto = new ProductUpdateDto(
            ProductId: product.ProductId,
            ProductName: "My Update",
            Price: product.Price,
            QtyInStock: product.QtyInStock,
            CategoryId: product.CtgryId,
            RowVersion: oldRowVersion
        );

        var response 
            = await client.PutAsJsonAsync( $"/api/products/{product.ProductId}", dto, TestContext.Current.CancellationToken );

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

}