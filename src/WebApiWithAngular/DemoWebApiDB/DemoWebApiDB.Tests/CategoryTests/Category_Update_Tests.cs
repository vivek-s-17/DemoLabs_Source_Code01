using System.Net;
using System.Net.Http.Json;
using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Categories;
using DemoWebApiDB.Tests.TestInfrastructure;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;


namespace DemoWebApiDB.Tests.CategoryTests;


/// <summary>
///     Integration tests for Category UPDATE endpoint.
///     
///     Demonstrates:
///     - concurrency testing
///     - duplicate detection
///     - validation safety
///     - full pipeline integration
/// </summary>
public sealed class Category_Update_Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Category_Update_Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }


    #region SUCCESS

    [Fact]
    public async Task UpdateCategory_Return202_WhenValid()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories.First();
        var rowVersion = Convert.ToBase64String(category.RowVersion);

        var dto = new CategoryUpdateDto(
            CategoryId: category.CategoryId,
            Name: "Updated Name",
            Description: "Updated description",
            RowVersion: rowVersion
        );

        // ----- Act 

        var response = await client.PutAsJsonAsync(
                $"/api/categories/{category.CategoryId}", dto, TestContext.Current.CancellationToken);

        // ------ Assert: response is 202 "Accepted"
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        // ----- Assert: verify if DB is updated
        using var verifyScope = factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var updated = verifyDb.Categories
                              .First(c => c.CategoryId == category.CategoryId);
        updated.Name.Should().Be("Updated Name");
    }

    #endregion


    #region ROUTE BODY MISMATCH

    [Fact]
    public async Task UpdateCategory_Return400_WhenRouteBodyMismatch()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories.First();
        var rowVersion = Convert.ToBase64String(category.RowVersion);

        var dto = new CategoryUpdateDto(
            CategoryId: 9999,               // wrong id provided
            Name: "Invalid",
            Description: "Invalid",
            RowVersion: rowVersion
        );

        // ----- Act
        var response = await client.PutAsJsonAsync(
            $"/api/categories/{category.CategoryId}", dto, TestContext.Current.CancellationToken);

        // ----- Arrange
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion


    #region NOT FOUND

    [Fact]
    public async Task UpdateCategory_Return404_WhenNotFound()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        var dto = new CategoryUpdateDto(
            CategoryId: 99999,                          // non-existent Category
            Name: "Does not exist",
            Description: "NA",
            RowVersion: Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        );

        // ----- Act
        var response = await client.PutAsJsonAsync(
            "/api/categories/99999", dto, TestContext.Current.CancellationToken);

        // ----- Assert: 404 "NotFound"
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion


    #region DUPLICATE

    [Fact]
    public async Task UpdateCategory_Return409_WhenDuplicateName()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var categories = db.Categories.Take(2).ToList();
        var first = categories[0];
        var second = categories[1];

        var dto = new CategoryUpdateDto(
            CategoryId: first.CategoryId,
            Name: second.Name,                      // duplicate
            Description: "Duplicate attempt",
            RowVersion: Convert.ToBase64String(first.RowVersion)
        );

        // ----- Act
        var response = await client.PutAsJsonAsync(
            $"/api/categories/{first.CategoryId}", dto, TestContext.Current.CancellationToken);

        // ----- Assert: 409 "Conflict"
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    #endregion


    #region CONCURRENCY

    [Fact]
    public async Task UpdateCategory_Return409_WhenConcurrencyConflict()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories.First();
        var originalRowVersion = Convert.ToBase64String(category.RowVersion);

        // ----- ACT: simulate another user update directly to the data in database
        category.Name = "Changed by another user";
        db.SaveChanges();

        // -----ACT: now attempt update with OLD rowversion 
        var dto = new CategoryUpdateDto(
            CategoryId: category.CategoryId,
            Name: "My update",
            Description: "Conflict test",
            RowVersion: originalRowVersion                  // old version
        );
        var response = await client.PutAsJsonAsync(
            $"/api/categories/{category.CategoryId}", dto, TestContext.Current.CancellationToken);

        // ----- ASSERT: 409 "Conflict"
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    #endregion

}