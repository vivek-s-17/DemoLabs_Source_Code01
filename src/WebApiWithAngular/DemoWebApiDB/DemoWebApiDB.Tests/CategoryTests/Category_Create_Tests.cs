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
        using var httpClient = factory.CreateClient();

        var dto = new CategoryCreateDto(
            Name: "Furniture",
            Description: "Home and office furniture"
        );

        // ----- Act
        var response 
            = await httpClient.PostAsJsonAsync("/api/categories", dto, TestContext.Current.CancellationToken);

        // ------ Assert HTTP status
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // ------ Assert Location header exists
        response.Headers.Location.Should().NotBeNull();

        // ----- Assert Location header returns the correct path
        var location = response.Headers.Location!.ToString();
        location.Should().StartWith("/api/categories/");

        // ----- Assert that the data was inserted into the database
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var exists = db.Categories.Any(c => c.Name == dto.Name);
        exists.Should().BeTrue();
    }

    #endregion


    #region VALIDATION FAILURE TESTS

    [Fact]
    public async Task CreateCategory_Return400_WhenInvalidPayload()
    {
        // ------ Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new CategoryCreateDto(
            Name: "   ",                            // invalid!
            Description: "Invalid category"
        );

        // ----- Act
        var response 
            = await client.PostAsJsonAsync( "/api/categories", dto, TestContext.Current.CancellationToken);

        // ----- Assert that the response is BadRequest
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // ----- Assert that the response provides ProblemDetails
        var problem 
            = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(TestContext.Current.CancellationToken);
        problem.Should().NotBeNull();

        // ----- Assert that the ProblemDetails provides info on Name
        problem!.Errors.Should().ContainKey("Name");
    }


    [Fact]
    public async Task CreateCategory_Return400_WhenWrongDatatype()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var json = """
        {
            "name": 123,
            "description": "test"
        }
        """;

        // ----- Act
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(
            "/api/categories",
            content,
            TestContext.Current.CancellationToken);

        // ----- Assert that the response is BadRequest
        //       NOTE: It won't have ProblemDetails since the payload was invalid.
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion


    #region CONFLICT TEST

    [Fact]
    public async Task CreateCategory_Return409_WhenDuplicate()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new CategoryCreateDto(
            Name: "Electronics",                    // Seed data contains Electronics (check TestDatabaseSeeder)
            Description: "Duplicate attempt"
        );

        // ----- Act
        var response 
            = await client.PostAsJsonAsync("/api/categories", dto, TestContext.Current.CancellationToken);

        // ----- Assert that response contains HTTP 409 "CONFLICT"
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    #endregion

}
