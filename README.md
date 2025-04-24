### **README: Backend (AllTheBeans API)**

# AllTheBeans Backend – ASP.NET Core API

This is the backend API for the AllTheBeans application, built using .NET 9, Entity Framework Core and MediatR.

It handles:

- Creating, retrieving, updating and deleting coffee beans
- Fetching the "Bean of the Day"

## Prerequisites

- .NET 9 SDK (https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (e.g. SQL Server Developer edition or Docker)

## Getting Started

1. Clone the repository or download the code.

2. Navigate to the root project folder and open the `.sln` file:

3. Open the appsettings.json file inside AllTheBeans.Api and update the connection string:

4. Apply the migration to create the database:
dotnet ef database update --project AllTheBeans.DataAccess --startup-project AllTheBeans

5. Run the application it should automatically open to Swagger
https://localhost:7025/swagger

## Testing

I have included some unit tests, you can find these in the test explorer no set up is needed.