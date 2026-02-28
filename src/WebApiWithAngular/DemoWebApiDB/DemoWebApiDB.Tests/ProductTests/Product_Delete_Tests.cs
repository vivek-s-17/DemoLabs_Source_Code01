using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Products;
using DemoWebApiDB.Tests.TestInfrastructure;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;


namespace DemoWebApiDB.Tests.ProductTests;


/// <summary>
///     Integration tests for Product DELETE endpoint.
///     
///     Demonstrates:
///     - Hard delete
///     - Concurrency validation
///     - Defensive API design
///     - Proper HTTP semantics
/// </summary>
public sealed class Product_Delete_Tests
{

    [Fact]
    public async Task DeleteProduct_ShouldReturn204_WhenValid()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();

        // ----- Act

        var dto = new ProductDeleteDto(
            ProductId: product.ProductId,
            RowVersion: Convert.ToBase64String(product.RowVersion)
        );

        var request 
            = new HttpRequestMessage( HttpMethod.Delete,
            $"/api/products/{product.ProductId}")
            {
                Content = JsonContent.Create(dto)
            };

        var response 
            = await client.SendAsync( request, TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // verify removed
        using var verifyScope = factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var exists = verifyDb.Products.Any(p => p.ProductId == product.ProductId);

        exists.Should().BeFalse();
    }


    [Fact]
    public async Task DeleteProduct_ShouldReturn400_WhenRouteBodyMismatch()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();

        // ----- Act

        var dto = new ProductDeleteDto(
            ProductId: Guid.NewGuid(), // mismatch
            RowVersion: Convert.ToBase64String(product.RowVersion)
        );

        var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/api/products/{product.ProductId}")
            {
                Content = JsonContent.Create(dto)
            };

        var response = await client.SendAsync( request, TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task DeleteProduct_ShouldReturn404_WhenNotFound()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new ProductDeleteDto(
            ProductId: Guid.NewGuid(),
            RowVersion: Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        );

        // ----- Assert

        var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/api/products/{dto.ProductId}")
        {
            Content = JsonContent.Create(dto)
        };

        var response 
            = await client.SendAsync( request, TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task DeleteProduct_ShouldReturn409_WhenConcurrencyConflict()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();
        var oldRowVersion = Convert.ToBase64String(product.RowVersion);

        // ----- Act - simulate another update
        product.ProductName = "Changed elsewhere";
        db.SaveChanges();

        // ----- Act - call the API endpoint

        var dto = new ProductDeleteDto(
            ProductId: product.ProductId,
            RowVersion: oldRowVersion
        );

        var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/api/products/{product.ProductId}")
        {
            Content = JsonContent.Create(dto)
        };

        var response 
            = await client.SendAsync( request, TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }


    [Fact]
    public async Task DeleteProduct_ShouldReturn400_WhenRowVersionInvalidFormat()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var product = db.Products.First();

        var dto = new ProductDeleteDto(
            ProductId: product.ProductId,
            RowVersion: "invalid-base64" // bad format
        );

        // ----- Act

        var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/api/products/{product.ProductId}")
        {
            Content = JsonContent.Create(dto)
        };

        var response = await client.SendAsync(
            request,
            TestContext.Current.CancellationToken);

        // ----- Assert

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

}