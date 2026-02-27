using System.Net;
using DemoWebApi.Tests.TestInfrastructure;
using FluentAssertions;


namespace DemoWebApi.Tests;

public class IntegrationTests1
{

    private readonly ITestOutputHelper _outputHelper;

    public IntegrationTests1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }


    [Fact]
    public async Task Test1_GET_SyncVersion()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var httpClient = factory.CreateClient();
        string expectedResult = "hello world from my API";

        // ----- 2. ACT
        var responseObject 
            = await httpClient.GetAsync("/api/ApiTest/", TestContext.Current.CancellationToken);

        // ----- 3(a). ASSERT (check the response is a valid HttpResponseMessage)
        Assert.IsType<HttpResponseMessage>(responseObject);

        // ----- 3(b). ASSERT (check if the Response Status Code is 200)
        responseObject.StatusCode.Should().Be(HttpStatusCode.OK);

        // ----- 3(c). ASSERT (check if response is NOT NULL)
        responseObject.Content.Should().NotBeNull();
        // Assert.NotNull(responseObject.Content);

        // ----- 3(d). ASSERT (check if response is as expected)
        var actualResult 
            = await responseObject.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        actualResult.Should().Be(expectedResult);
        // Assert.Equal(expectedResult, actualResult);

        _outputHelper.WriteLine($"Received: {expectedResult}");
    }


    [Fact]
    public async Task Test2_GET_AsyncVersion()
    {
        // ----- Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        string expectedResult = "hello world from my API";

        // ----- 2. ACT
        var responseObject
            = await client.GetAsync("/api/ApiTest/GetAsync", TestContext.Current.CancellationToken);

        // ----- 3(a). ASSERT (check the response is a valid HttpResponseMessage)
        Assert.IsType<HttpResponseMessage>(responseObject);

        // ----- 3(b). ASSERT (check if the Response Status Code is 200)
        responseObject.StatusCode.Should().Be(HttpStatusCode.OK);

        // ----- 3(c). ASSERT (check if response is NOT NULL)
        responseObject.Content.Should().NotBeNull();

        // ----- 3(d). ASSERT (check if response is as expected)
        var actualResult
            = await responseObject.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        actualResult.Should().Be(expectedResult);

        _outputHelper.WriteLine($"Received: {expectedResult}");
    }

}
