using System.Net;
using System.Net.Http.Json;
using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Categories;
using DemoWebApiDB.Tests.TestInfrastructure;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;


namespace DemoWebApiDB.Tests.CategoryTests;


/// <summary>
///     Integration tests for Category DELETE endpoint.
///
///     Demonstrates:
///     - safe deletion
///     - concurrency validation
///     - referential integrity checks
///     - full HTTP pipeline testing
/// </summary>
/// <remarks>
///     HTTP spec never standardized DELETE body handling.
///     So frameworks:
///         - support POST/PUT with body easily
///         - but DELETE requires manual request
///     Real-world APIs often:
///     - send RowVersion in DELETE body
///     - send audit reason
///     - send soft-delete metadata
///
///     For a cleaner Delete, I am using the custom HttpClientExtensions.DeleteJsonAsync() method.
/// </remarks>
public sealed class Category_Delete_Tests    
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Category_Delete_Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }


    #region SUCCESS DELETE

    [Fact]
    public async Task DeleteCategory_Return204_WhenValid()
    {
        // ----- Arrange - pick category WITHOUT products
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories
            .First(c => !db.Products.Any(p => p.CtgryId == c.CategoryId));

        var dto = new CategoryDeleteDto(
            CategoryId: category.CategoryId,
            RowVersion: Convert.ToBase64String(category.RowVersion)
        );

        // ----- Act

        var request 
            = new HttpRequestMessage( HttpMethod.Delete, $"/api/categories/{category.CategoryId}" )
            {
                Content = JsonContent.Create(dto)
            };
        var response 
            = await client.SendAsync( request, TestContext.Current.CancellationToken );

        /*****************************
            // The extension method version of the above lines:

            var response = await client.DeleteJsonAsync(
                $"/api/categories/{category.CategoryId}", dto, TestContext.Current.CancellationToken);
        **************/

        // ---- Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // ----- Verify removed from DB
        using var verifyScope = factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var exists = verifyDb.Categories.Any(c => c.CategoryId == category.CategoryId);
        exists.Should().BeFalse();
    }

    #endregion


    #region ROUTE BODY MISMATCH

    [Fact]
    public async Task DeleteCategory_Return400_WhenRouteBodyMismatch()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories.First();

        var dto = new CategoryDeleteDto(
            CategoryId: 9999,
            RowVersion: Convert.ToBase64String(category.RowVersion)
        );

        var request
            = new HttpRequestMessage(HttpMethod.Delete, $"/api/categories/{category.CategoryId}")
            {
                Content = JsonContent.Create(dto)
            };
        var response
            = await client.SendAsync(request, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion


    #region NOT FOUND

    [Fact]
    public async Task DeleteCategory_Return404_WhenNotFound()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new CategoryDeleteDto(
            CategoryId: 99999,
            RowVersion: Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        );

        var request
            = new HttpRequestMessage(HttpMethod.Delete, $"/api/categories/{dto.CategoryId}")
            {
                Content = JsonContent.Create(dto)
            };
        var response
            = await client.SendAsync(request, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion


    #region RESTRICT DELETE

    [Fact]
    public async Task DeleteCategory_Return409_WhenProductsExist()
    {
        // ----- Arrange - category WITH products
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories
            .First(c => db.Products.Any(p => p.CtgryId == c.CategoryId));

        var dto = new CategoryDeleteDto(
            CategoryId: category.CategoryId,
            RowVersion: Convert.ToBase64String(category.RowVersion)
        );

        // ----- Act
        var request
            = new HttpRequestMessage(HttpMethod.Delete, $"/api/categories/{category.CategoryId}")
            {
                Content = JsonContent.Create(dto)
            };
        var response
            = await client.SendAsync(request, TestContext.Current.CancellationToken);

        // ----- Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    #endregion


    #region CONCURRENCY DELETE

    [Fact]
    public async Task DeleteCategory_Return409_WhenConcurrencyConflict()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories
            .First(c => !db.Products.Any(p => p.CtgryId == c.CategoryId));

        var oldRowVersion = Convert.ToBase64String(category.RowVersion);

        // simulate another update
        category.Name = "Changed elsewhere";
        db.SaveChanges();

        var dto = new CategoryDeleteDto(
            CategoryId: category.CategoryId,
            RowVersion: oldRowVersion
        );

        // ----- Act
        var request
            = new HttpRequestMessage(HttpMethod.Delete, $"/api/categories/{category.CategoryId}")
            {
                Content = JsonContent.Create(dto)
            };
        var response
            = await client.SendAsync(request, TestContext.Current.CancellationToken);

        // ----- Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    #endregion

}
