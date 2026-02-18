// STEPS:
//      1. Create a new "ASP.NET WEB API" project
// NOTE: 
//    1. To view the endpoints:
//          View > Other Windows > Endpoints Explorer.
//       Right-click on the endpoint, and "GENERATE REQUEST"
//    2. In Browser, navigate to: https://localhost:XXXX/WeatherForecast
//    3. Use the .HTTP file to test


var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

// builder.Services.AddControllers();

builder.Services
    .AddControllers( options =>
    {
        // Respect the Accept header sent by the browser/client
        options.RespectBrowserAcceptHeader = true;

        // Return 406 Not Acceptable if the client requests an unsupported format
        options.ReturnHttpNotAcceptable = true;

    } )
    .AddXmlSerializerFormatters();               // Add support for XML serialization


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi();


var app = builder.Build();



// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{

    // OPEN API documentation is accessable at: https://localhost:7260/openapi/v1.json
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// My Middleware
app.Use( async ( context, next ) =>
{

   // context.Response.Headers.Append( "Author", "Manoj Kumar Sharma" );

    // Call the next delegate/middleware in the pipeline
    await next();

    // do something else also (after the next middleware has executed)

} );


// Minimal API Version!
app.MapGet( "/Author", () => "Manoj Kumar Sharma" );


app.Run();

