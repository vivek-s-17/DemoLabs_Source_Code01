# xUnit Testing Project (xUnit Tests) - xUnit Test Project 

IMPORTANT NOTE:
The default xUnit v2.9 Nuget Package in the Project Template could be deprecated. 
Uninstall it, and install the recommended "packageid:xunit.v3"


This project demonstrates Integration Tests with WebApplicationFactory to test:
- Controllers
- Services
- EF Core
- Model validation
- Filters
- Middleware
- Result pattern
- ProblemDetails
- Routing
- Serialization


The Tests can use any of the following options for data store:

Option A — EF InMemory Provider (fastest, but not realistic)
- No SQL constraints
- No unique indexes
- No concurrency behaviour
- Can give false positives

Option B — SQLite in-memory ⭐ RECOMMENDED
- Real relational DB behaviour
- Enforces constraints
- Supports concurrency
- Very fast
- Used in enterprise testing

Option C — Real SQL Server
- Slow
- Heavy
- Not ideal for learning purposes


# Tests:
- POST success
- Duplicate detection
- Not found
- Auth failure
- Validation errors

Using:
- WebApplicationFactory
- InMemory EF


