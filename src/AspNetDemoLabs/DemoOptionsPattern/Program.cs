using DemoOptionsPattern.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

// SEE: https://localhost:xxxx/openapi/v1.json
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// DEFAULT: builder.Services.AddOpenApi();
builder.Services
    .AddOpenApi(configureOptions =>
    {
        configureOptions.AddDocumentTransformer((document, context, cancellationToken) => {

            document.Info.Title = "My Demo API";
            document.Info.Contact = new Microsoft.OpenApi.OpenApiContact
            {
                Name = "Manoj Kumar Sharma",
                Email = "mailme@manojkumarsharma.com"
            };

            return Task.CompletedTask;
        });
    });

// Register the Configuration typed options model into the application services DI container 
builder.Services
    .AddOptions<MyAppOptionsModel>()
    .BindConfiguration("MyAppOptions")                  // section name in appsettings.json file
    .ValidateDataAnnotations()                          // to enforce validation rules
    .ValidateOnStart()                                  // (optional) add if needed.
    .Validate(options =>                                // custom validation!
    {
        if (options.TrainerName == "Manoj Kumar Sharma")
        {
            return true;
        }

        // throws the Microsoft.Extensions.Options.OptionsValidationException:
        // with the default message: 'A validation error has occurred.'
        return false;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // exposes https://localhost:7091/openapi/v1.json
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// To add Swagger for API Documentation:
// (a) Add the Nuget Package: Swashbuckle.AspNetCore.SwaggerUI
// (b) Register SwaggerUI Middleware
//          if (app.Environment.IsDevelopment())
//          {
//              app.UseSwaggerUI(options => 
//              {
//                  options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
//              });
//          }
// (c) Access the URL: https://localhost:7091/swagger/index.html


// Or, use ReDoc for API Documentation:
// (a) Add the Nuget Package: Swashbuckle.AspNetCore.ReDoc 
// (b) Register the ReDoc Middleware
//          if (app.Environment.IsDevelopment())
//          {
//              app.UseReDoc(options => 
//              {
//                  options.SpecUrl("/openapi/v1.json");
//              });
//          }
// (c) Access the URL: https://localhost:7091/api-docs/index.html


// Or, use Scalar
// (a) Add the Nuget Package: Scalar.AspNetCore 
// (b) Register Scalar Middleware
// (c) Access the URL: https://localhost:7091/scalar/v1
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}



// Example: To access strongly typed configuration inside Program.cs:
var myAppOptions = builder.Configuration
                          .GetSection("MyAppOptions")
                          .Get<MyAppOptionsModel>();
var trainer = myAppOptions?.TrainerName;


app.Run();
