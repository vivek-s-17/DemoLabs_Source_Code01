using System.Net;
using System.Net.Http.Json;

using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Categories;
using DemoWebApiDB.Tests.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DemoWebApiDB.Tests.CategoryTests;


/// <summary>
///     Integration tests for Category CREATE endpoint.
///     
///     Demonstrates:
///     - Success path testing
///     - Validation failures
///     - Conflict detection
///     - Database verification
/// </summary>
public sealed class Category_Create_Tests
    : IClassFixture<CustomWebApplicationFactory>
{

    private readonly ITestOutputHelper _testOutputHelper;

    public Category_Create_Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    
    #region SUCCESS TEST

    [Fact]
    public async Task CreateCategory_Return201_WhenValid()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new CategoryCreateDto(
            Name: "Furniture",
            Description: "Home and office furniture"
        );

        // ----- Act
        var response 
            = await client.PostAsJsonAsync("/api/categories", dto, TestContext.Current.CancellationToken);

        // ------ Assert HTTP status
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // ------ Assert Location header exists
        response.Headers.Location.Should().NotBeNull();

        var location = response.Headers.Location!.ToString();
        location.Should().StartWith("/api/categories/");

        // Verify inserted into database
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var exists = db.Categories.Any(c => c.Name == "Furniture");
        exists.Should().BeTrue();
    }

    #endregion


    #region VALIDATION FAILURE TESTS

    [Fact]
    public async Task CreateCategory_Return400_WhenInvalidPayload()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new CategoryCreateDto(
            Name: "   ",                            // invalid!
            Description: "Invalid category"
        );

        var response 
            = await client.PostAsJsonAsync( "/api/categories", dto, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problem 
            = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(TestContext.Current.CancellationToken);

        problem.Should().NotBeNull();
        problem!.Errors.Should().ContainKey("Name");
    }


    [Fact]
    public async Task CreateCategory_Return400_WhenWrongDatatype()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var json = """
        {
            "name": 123,
            "description": "test"
        }
        """;

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync(
            "/api/categories",
            content,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion


    #region CONFLICT TEST

    [Fact]
    public async Task CreateCategory_Return409_WhenDuplicate()
    {
        // Seed already contains Electronics (from TestDatabaseSeeder)
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new CategoryCreateDto(
            Name: "Electronics",
            Description: "Duplicate attempt"
        );

        var response 
            = await client.PostAsJsonAsync("/api/categories", dto, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    #endregion

}
