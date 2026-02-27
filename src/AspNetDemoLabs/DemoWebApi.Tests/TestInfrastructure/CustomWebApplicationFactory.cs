using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;


namespace DemoWebApi.Tests.TestInfrastructure;


/// <summary>
///     CustomWebApplicationFactory is used for integration testing.
/// </summary>
/// <remarks>
///  VERY IMPORTANT NOTE:
///     WebApplicationFactory spins up the real API inside the test runner.
///     It uses the "testhost.deps.json" file to inform:
///     - what assemblies exist
///     - what dependencies to load
///     - how to boot the app.
///     SOLUTION: 
///     (a) Configure the API Project to preserve compilation metadata, by adding the following in .CSPROJ file:
///             <PreserveCompilationContext>true</PreserveCompilationContext>
///     (b) Add Project Reference to API Project, in the Test Project.
/// </remarks>
public sealed class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");              // 🔥 CRITICAL LINE

        builder.ConfigureServices(services =>
        {
        });
    }


    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }

}