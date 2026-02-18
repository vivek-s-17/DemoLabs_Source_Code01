using Microsoft.AspNetCore.Mvc.Formatters;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//------- Register needed services to the DI container.

// NOTE: This should be always first!: Define how to connect to your database!
const string ConnectionStringNAME = "DefaultConnectionString";
string connectionString 
    = builder.Configuration.GetConnectionString(ConnectionStringNAME)
      ?? throw new Exception($"Connection String '{ConnectionStringNAME}' not defined in appsettings file");
// TODO: Configure DB Context to use SQL Server!


// builder.Services.AddControllers();

builder.Services
    .AddControllers(options =>
    {
        // Respect the Accept header sent by the browser/client
        options.RespectBrowserAcceptHeader = true;

        // Return 406 Not Acceptable if the client requests an unsupported format
        options.ReturnHttpNotAcceptable = true;

        // Enable support to define the Character-set in the Request "Accept" parameter
        var jsonOutputFormatter
             = options.OutputFormatters.OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();
        jsonOutputFormatter?.SupportedMediaTypes.Add("application/json; charset=utf-8");

    })
    .AddJsonOptions(options =>
    {
        // Throw an exception if the JSON contains properties that are not in the model
        options.JsonSerializerOptions.UnmappedMemberHandling
            = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow;
    })
    .AddXmlSerializerFormatters();               // Add support for XML serialization


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
