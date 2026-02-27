using System.Net.Http.Json;

namespace DemoWebApiDB.Tests.TestInfrastructure;

internal static class HttpClientExtensions
{

    public static async Task<HttpResponseMessage> DeleteJsonAsync<T>(
        this HttpClient client,
        string url,
        T body,
        CancellationToken token = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url)
        {
            Content = JsonContent.Create(body)
        };

        return await client.SendAsync(request, token);
    }

}
