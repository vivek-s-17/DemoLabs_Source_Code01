using System.Net;
using System.Net.Http.Json;

using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Categories;
using DemoWebApiDB.Tests.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        // ----- Arrange - pick categoryToDelete WITHOUT products
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Pick a Category that does not have any products.
        var categoryToDelete = db.Categories
            .First(c => !db.Products.Any(p => p.CtgryId == c.CategoryId));

        // Prepare the payload data object
        var dto = new CategoryDeleteDto(
            CategoryId: categoryToDelete.CategoryId,
            RowVersion: Convert.ToBase64String(categoryToDelete.RowVersion)
        );

        // ----- Act
        var request
            = new HttpRequestMessage(HttpMethod.Delete, $"/api/categories/{categoryToDelete.CategoryId}")
            {
                Content = JsonContent.Create(dto)
            };
        var response 
            = await client.SendAsync( request, TestContext.Current.CancellationToken );

        /*****************************
            // The extension method version of the above lines:

            var response = await client.DeleteJsonAsync(
                $"/api/categories/{categoryToDelete.CategoryId}", dto, TestContext.Current.CancellationToken);
        **************/

        // ----- Assert that Delete was successful, and HTTP 204 "NoContent" is received.
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // ---- Assert if the row was deleted from the database
        using var verifyScope = factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var exists = verifyDb.Categories.Any(c => c.CategoryId == categoryToDelete.CategoryId);
        exists.Should().BeFalse();
    }

    #endregion


    #region ROUTE BODY MISMATCH

    [Fact]
    public async Task DeleteCategory_Return400_WhenRouteBodyMismatch()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Prepare the payload data - pick a category and mutate it
        var category = db.Categories.First();
        var dto = new CategoryDeleteDto(
            CategoryId: 9999,
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

        // ---- Assert: response is HTTP 400 "BadRequest", since its not found!
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // ----- Assert: ProblemDetails structure
        var problem 
            = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>( TestContext.Current.CancellationToken );
        problem.Should().NotBeNull();

        // ----- Assert: RFC 7807 fields
        problem!.Status.Should().Be(StatusCodes.Status400BadRequest);
        problem.Title.Should().NotBeNullOrWhiteSpace();

        // ----- Assert: Validation error exists for CategoryId
        problem.Errors.Should().ContainKey("CategoryId");

        // ----- Assert: Correct error message
        problem.Errors["CategoryId"]
            .Should()
            .Contain("Route ID and payload ID must match.");
    }

    #endregion


    #region NOT FOUND

    [Fact]
    public async Task DeleteCategory_Return404_WhenNotFound()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new CategoryDeleteDto(
            CategoryId: 99999,                  // non-existent Category ID
            RowVersion: Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        );

        // ----- Act
        var request
            = new HttpRequestMessage(HttpMethod.Delete, $"/api/categories/{dto.CategoryId}")
            {
                Content = JsonContent.Create(dto)
            };
        var response
            = await client.SendAsync(request, TestContext.Current.CancellationToken);

        // ---- Assert: if response is 404 "NotFound"
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion


    #region RESTRICT DELETE

    [Fact]
    public async Task DeleteCategory_Return409_WhenProductsExist()
    {
        // ----- Arrange - categoryToDelete WITH products
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
        // ---- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories
            .First(c => !db.Products.Any(p => p.CtgryId == c.CategoryId));

        var oldRowVersion = Convert.ToBase64String(category.RowVersion);

        var dto = new CategoryDeleteDto(
            CategoryId: category.CategoryId,
            RowVersion: oldRowVersion
        );


        // ----- Act: Simulate another update, by updating the database directly - will change Name & RowVersion
        category.Name = "Changed elsewhere";
        db.SaveChanges();


        // ----- Act: Submit Delete request to API
        var request
            = new HttpRequestMessage(HttpMethod.Delete, $"/api/categories/{category.CategoryId}")
            {
                Content = JsonContent.Create(dto)
            };
        var response
            = await client.SendAsync(request, TestContext.Current.CancellationToken);

        // ----- Assert: response is 409 "Conflict"
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    #endregion

}
