1. Create a new ASP.NET WEB API project
2. Added the Nuget Package for Scalar & Register it in Program.cs (see below)
3. Configure the Program.cs to validate JSON & XML Serialization
4. In the "appsettings.json" file, configure the Connection String (see below)
5. Add the needed EF packages 
6. In Program.cs configure SQL Server 






-----------------------------

To register Scalar UI documentation for Open API
    1. Install the "Scalar.AspNetCore" Nuget package (LATEST VERSION)
    2. In Program.cs
        builder.Services.AddOpenApi();              // Add OpenAPI support 
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.MapScalarApiReference();            // Enable Scalar UI middleware
    }
    3. Navigate to "/scalar" (Eg: https://localhost:xxxx/scalar)

-----------------------------

To configure the connection string:
1. Open View->SQL Object Explorer
2. Right-click and add SQL Server (to add the connection)
    (a) Select the Server "."
    (b) Authentication: Windows OR userid/password
    (c) Check the "Trust Certificate"
    (d) Encryption: "Optional"
3. Select the Servername, and right-click to see "Properties"
    - Copy-paste the "ConnectionString" to "appsettings.json" under the ConnectionString section!
    - Add the Database information: "Initial Catalog: TriviumDB;"
4. Add "Microsoft.EntityFrameworkCore.SqlServer" Nuget Package 
    (latest version for the .NET VERSION OF PROJECT)
5. Add "Microsoft.EntityFrameworkCore.Tools" Nuget Package 
    (same version as the version of "Microsoft.EntityFrameworkCore.SqlServer" Nuget Package) 


