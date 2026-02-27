using System.Net;
using System.Net.Http.Json;
using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Categories;
using DemoWebApiDB.Tests.TestInfrastructure;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;


namespace DemoWebApiDB.Tests.CategoryTests;


/// <summary>
///     Integration tests for GET ALL Categories endpoint.
/// 
///     Demonstrates:
///     - deterministic ordering
///     - DTO correctness
///     - audit fields returned
///     - HTTP 200 verification
/// </summary>
public sealed class Category_GetAll_Tests
{
    [Fact]
    public async Task GetAllCategories_Return200_AndOrderedList()
    {
        // ---------- Arrange ----------

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        // ---------- Act ----------

        var response = await client.GetAsync(
            "/api/categories",
            TestContext.Current.CancellationToken);

        // ---------- Assert: HTTP ----------

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var categories 
            = await response.Content.ReadFromJsonAsync<IReadOnlyList<CategoryReadDto>>(
                cancellationToken: TestContext.Current.CancellationToken);

        categories.Should().NotBeNull();
        categories.Should().NotBeEmpty();

        // ---------- Assert: Ordering ----------
        categories!
            .Select(c => c.Name)
            .Should()
            .BeInAscendingOrder();

        // ---------- Assert: DTO contains audit fields ----------

        categories!.First().CreatedAtUtc.Should().NotBe(default);
        categories!.First().RowVersion.Should().NotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task GetAllCategories_ReturnEmptyList_WhenNoCategoriesExist()
    {
        // ---------- Arrange ----------

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // remove any of the existing categories that may have been seeded!
        db.Categories.RemoveRange(db.Categories);
        db.SaveChanges();


        // ---------- Act ----------

        var response = await client.GetAsync(
            "/api/categories",
            TestContext.Current.CancellationToken);

        // ---------- Assert: HTTP ----------

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // ---------- Assert: No Data ----------

        var categories = await response.Content
            .ReadFromJsonAsync<IReadOnlyList<CategoryReadDto>>(
                cancellationToken: TestContext.Current.CancellationToken);

        categories.Should().NotBeNull();
        categories.Should().BeEmpty();
    }

}