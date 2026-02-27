using System.Net;
using System.Net.Http.Json;
using DemoWebApiDB.Tests.TestInfrastructure;
using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Categories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;


namespace DemoWebApiDB.Tests.CategoryTests;

/// <summary>
///     Integration tests for GET Category by ID endpoint.
///     
///     Demonstrates:
///     - DTO mapping validation
///     - audit field validation
///     - HTTP semantics
/// </summary>
public sealed class Category_GetById_Tests
{

    [Fact]
    public async Task GetCategoryById_Return200_WhenCategoryExists()
    {
        // ---------- Arrange ----------
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var category = db.Categories.First();

        // ---------- Act ----------
        var response = await client.GetAsync(
            $"/api/categories/{category.CategoryId}", TestContext.Current.CancellationToken);

        // ---------- Assert HTTP ----------
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<CategoryReadDto>(
            cancellationToken: TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();

        // ---------- Assert data ----------
        dto!.CategoryId.Should().Be(category.CategoryId);
        dto.Name.Should().Be(category.Name);

        // ---------- Assert audit ----------
        dto.CreatedAtUtc.Should().NotBe(default);
        dto.RowVersion.Should().NotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task GetCategoryById_Return404_WhenNotFound()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync(
            "/api/categories/999999",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task GetCategoryById_Return404_WhenIdIsNotInteger()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync(
            "/api/categories/abc",
            TestContext.Current.CancellationToken);

        // NOTE: This is a 404 "Not Found" error, not 400 "Bad Request"
        // REASON: The endpoint does not exist.
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

}