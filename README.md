# Task Management API Project Challenge

This is a .NET 9 Web API project for managing user tasks. The API provides functionality to create, read, update, and delete tasks with support for task organization and tracking. Built using Entity Framework Core with MySQL database integration and documented with Swagger UI.

## Features

- **Task Management**: CRUD operations for user tasks
- **Database Integration**: MySQL database with Entity Framework Core
- **API Documentation**: Interactive Swagger UI for testing endpoints
- **Model Structure**: UserTask model with properties for Id, Title, Description, Date, and Status
- **Development Environment**: Configured for local development with MySQL

## Configure Swagger

- Add package
```shell
    dotnet add package Swashbuckle.AspNetCore
    dotnet restore
```

- Remove AddOpenApi() and MapOpenApi() and add UseSwagger() and UseSwaggerUI()
```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

- Swagger as default page
```csharp
    app.MapGet("/", () => Results.Redirect("/swagger")); 
```

## Configure DBContext

- Install Microsoft.EntityFrameworkCore package
```shell
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet restore
```

- Create DbContext file (`Context/OrganizerContext.cs`)
```csharp
public class OrganizerContext : DbContext
{
    public OrganizerContext(DbContextOptions<OrganizerContext> options) : base(options) { }
    
    public DbSet<UserTask> UserTasks { get; set; } = default!;
}
```

- Add connection string in `appsettings.Development.json`
```json
{
  "ConnectionStrings": {
    "MySqlConnection": "server=localhost;port=3306;database=organizerdb;user=root;password="
  }
}
```

- Configure DbContext in `Program.cs`
```csharp
builder.Services.AddDbContext<OrganizerContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
    new MySqlServerVersion(new Version(8, 0, 26)))
);
```

## Project Structure

```
DotnetTaskApi/
├── Controllers/
│   └── TaskController.cs      # API endpoints for task management
├── Models/
│   └── UserTask.cs           # Task entity model
├── Context/
│   └── OrganizerContext.cs   # Entity Framework DbContext
├── DTOs/                     # Data Transfer Objects (future use)
├── Program.cs                # Application entry point and configuration
├── appsettings.json          # Application settings
└── appsettings.Development.json # Development environment settings
```

## Getting Started

### Prerequisites
- .NET 9 SDK
- MySQL Server (version 8.0+)
- IDE (Visual Studio, VS Code, or JetBrains Rider)

### Installation

1. Clone the repository
2. Navigate to the project directory
3. Restore NuGet packages:
   ```shell
   dotnet restore
   ```
4. Update the MySQL connection string in `appsettings.Development.json`
5. Run the application:
   ```shell
   dotnet run
   ```
6. Navigate to `https://localhost:7xxx` (port will be displayed in terminal) to access Swagger UI

### Database Setup

1. Ensure MySQL server is running
2. Create the database:
   ```sql
   CREATE DATABASE organizerdb;
   ```
3. Create Entity Framework migration:
   ```shell
   dotnet ef migrations add CreateTaskTable
   ```
4. Run Entity Framework migrations:
   ```shell
   dotnet ef database update
   ```

## API Endpoints

The API will include the following endpoints for task management:

- `GET /api/tasks` - Retrieve all tasks
- `GET /api/tasks/{id}` - Retrieve a specific task
- `POST /api/tasks` - Create a new task
- `PUT /api/tasks/{id}` - Update an existing task
- `DELETE /api/tasks/{id}` - Delete a task

## UserTask Model

```csharp
public class UserTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<string> Status { get; set; }
}
```

## Technologies Used

- **.NET 9**: Latest .NET framework
- **ASP.NET Core Web API**: For building RESTful APIs
- **Entity Framework Core**: ORM for database operations
- **MySQL**: Database management system
- **Pomelo.EntityFrameworkCore.MySql**: MySQL provider for Entity Framework
- **Swashbuckle.AspNetCore**: Swagger/OpenAPI documentation generation
