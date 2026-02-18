
namespace DemoWebApp1.SwaggerDocs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable the OpenAPI middleware to generate the API documentation JSON file
                app.MapOpenApi();                   // https://localhost:xxxx/openapi/v1.json

                // Enable Swagger UI middleware
                app.UseSwaggerUI((options) =>
                {
                    options.SwaggerEndpoint(url:"/openapi/v1.json", name:"My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
