using DemoWebApiDB.Data.Data;
using DemoWebApiDB.DtoModels.Categories;
using DemoWebApiDB.DtoModels.ReadModels.Reports;
using DemoWebApiDB.Tests.TestInfrastructure;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace DemoWebApiDB.Tests.CategoryTests;


/// <summary>
///     Integration tests for Category Product Count STORED PROCEDURE endpoint.
/// 
///     VERY IMPORTANT NOTE:
///      SQLite, does not support Stored Procedures. 
///      So, on entry, I am checking if we are using SQLite, and am exiting!
/// 
///     Demonstrates:
///     - EF Core stored procedure execution
///     - Read-only reporting models
///     - Reporting validation discipline
/// </summary>
public sealed class Category_Report_Tests
{

    [Fact]
    public async Task GetCategoryProductCount_ShouldReturn200_AndData()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        //---- Check if running with SQLite in test environment, and exit since Stored Procedures not supported.
        if(db.Database.IsSqlite())
        {

        }


        // ----- Act

        var response = await client.GetAsync( "/api/categories/reports/product-count", TestContext.Current.CancellationToken);

        // ------ Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<IReadOnlyList<CategoryProductCountReadModel>>(
            cancellationToken: TestContext.Current.CancellationToken);

        data.Should().NotBeNull();
        data.Should().NotBeEmpty();
    }




    [Fact]
    public async Task GetCategoryProductCount_ShouldMatchDatabaseCounts()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // compute expected result directly from DB
        var expected = db.Categories
            .Select(c => new
            {
                c.CategoryId,
                c.Name,
                Count = db.Products.Count(p => p.CtgryId == c.CategoryId)
            })
            .ToList();

        // ----- Act

        var response 
            = await client.GetAsync( "/api/categories/reports/product-count", TestContext.Current.CancellationToken);

        // ------ Assert

        var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<CategoryProductCountReadModel>>(
            cancellationToken: TestContext.Current.CancellationToken);

        foreach (var expectedRow in expected)
        {
            var actual = result!.First(r => r.CategoryId == expectedRow.CategoryId);

            actual.CategoryName.Should().Be(expectedRow.Name);
            actual.ProductCount.Should().Be(expectedRow.Count);
        }
    }


    [Fact]
    public async Task GetCategoryProductCount_ShouldReturnZeroCounts_WhenNoProductsExist()
    {
        // ----- Arrange

        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        db.Products.RemoveRange(db.Products);
        db.SaveChanges();

        // ------ Act
        var response 
            = await client.GetAsync( "/api/categories/reports/product-count", TestContext.Current.CancellationToken);


        // ------ Assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<CategoryProductCountReadModel>>(
            cancellationToken: TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result.Should().OnlyContain(r => r.ProductCount == 0);
    }

}