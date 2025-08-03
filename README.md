# C# Web Application with MS SQL Database

A complete ASP.NET Core MVC web application that demonstrates how to retrieve and manage data from a Microsoft SQL Server database using Entity Framework Core.

## Features

- **Product Management System**: Full CRUD operations (Create, Read, Update, Delete)
- **MS SQL Database Integration**: Using Entity Framework Core with SQL Server
- **Modern Web UI**: Bootstrap 5 responsive design
- **Model Validation**: Client-side and server-side validation
- **Seeded Sample Data**: Pre-populated with sample products

## Technology Stack

- **Framework**: ASP.NET Core 8.0 (MVC)
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core 8.0
- **Frontend**: HTML5, CSS3, Bootstrap 5, JavaScript
- **Validation**: Data Annotations with jQuery Unobtrusive Validation

## Project Structure

```
WebApp/
├── Controllers/
│   └── ProductsController.cs      # Handles HTTP requests for product operations
├── Data/
│   └── ApplicationDbContext.cs    # EF Core database context
├── Models/
│   └── Product.cs                 # Product entity model
├── Views/
│   ├── Products/                  # Product-related views
│   │   ├── Index.cshtml          # Product listing
│   │   ├── Create.cshtml         # Add new product
│   │   ├── Details.cshtml        # View product details
│   │   ├── Edit.cshtml           # Edit product
│   │   └── Delete.cshtml         # Delete confirmation
│   ├── Shared/
│   │   ├── _Layout.cshtml        # Main layout template
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── _ViewImports.cshtml       # Global view imports
│   └── _ViewStart.cshtml         # Default layout setting
├── wwwroot/                       # Static web assets
│   ├── css/site.css              # Custom styles
│   └── js/site.js                # Custom JavaScript
├── appsettings.json              # Configuration including connection string
├── Program.cs                    # Application entry point
└── WebApp.csproj                 # Project file with dependencies
```

## Prerequisites

- .NET 8.0 SDK
- Microsoft SQL Server (LocalDB, Express, or full version)
- Visual Studio, VS Code, or any C# IDE

## Setup Instructions

### 1. Install .NET 8.0 SDK
Download and install from: https://dotnet.microsoft.com/download/dotnet/8.0

### 2. Database Setup
The application is configured to work with both SQLite and SQL Server:

**Option A: SQLite (Default - Cross-platform)**
- No additional setup required
- Database file (`WebApp.db`) is created automatically
- Perfect for development and testing

**Option B: SQL Server (Windows/Production)**
- Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YourServerName;Database=WebAppDb;Integrated Security=true;TrustServerCertificate=true"
  }
}
```

### 3. Restore Dependencies
```bash
dotnet restore
```

### 4. Run the Application
```bash
# Option 1: Using the provided script
./run.sh

# Option 2: Direct command
dotnet run
```

The application will:
- Automatically create the SQLite database (`WebApp.db`) if it doesn't exist
- Seed sample data (3 products)
- Launch on `http://localhost:5000` (or the port shown in console)

## Database Schema

### Products Table
| Column      | Type          | Description                    |
|-------------|---------------|--------------------------------|
| Id          | int (PK)      | Primary key, auto-increment    |
| Name        | nvarchar(100) | Product name (required)        |
| Description | nvarchar(500) | Product description (optional) |
| Price       | decimal(18,2) | Product price                  |
| Quantity    | int           | Available quantity             |
| CreatedDate | datetime2     | Record creation timestamp      |

## Key Features Demonstrated

### 1. Database Operations
- **Entity Framework Core**: Code-first approach with migrations
- **CRUD Operations**: All basic database operations
- **Async/Await**: Asynchronous database operations for better performance
- **Data Validation**: Model validation with annotations

### 2. MVC Architecture
- **Models**: Product entity with validation attributes
- **Views**: Razor views with strongly-typed models
- **Controllers**: Action methods handling HTTP requests

### 3. User Interface
- **Responsive Design**: Bootstrap 5 for mobile-friendly UI
- **Form Validation**: Client and server-side validation
- **CRUD Interface**: Complete user interface for data management

## Usage Examples

### Retrieving Data from Database
The `ProductsController.Index()` method demonstrates how to retrieve data:

```csharp
public async Task<IActionResult> Index()
{
    var products = await _context.Products.ToListAsync();
    return View(products);
}
```

### Creating New Records
The `ProductsController.Create()` method shows data insertion:

```csharp
[HttpPost]
public async Task<IActionResult> Create(Product product)
{
    if (ModelState.IsValid)
    {
        _context.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(product);
}
```

## Configuration

### Connection String
Update `appsettings.json` to change database connection:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=WebApp.db",
    "SqlServerConnection": "Server=YourServerName;Database=WebAppDb;Integrated Security=true"
  }
}
```

### Database Initialization
The application automatically creates the database and seeds sample data on startup (see `Program.cs`).

## Development Commands

```bash
# Restore packages
dotnet restore

# Build the application
dotnet build

# Run the application
dotnet run

# Run in development mode with hot reload
dotnet watch run

# Create a new migration (if you modify models)
dotnet ef migrations add MigrationName

# Update database with migrations
dotnet ef database update
```

## Troubleshooting

### Common Issues

1. **Database Connection Errors**
   - Verify SQL Server is running
   - Check connection string in `appsettings.json`
   - Ensure database server allows connections

2. **Port Already in Use**
   - The application will automatically find an available port
   - Check console output for the actual URL

3. **Package Restore Errors**
   - Run `dotnet restore` to reinstall packages
   - Clear NuGet cache: `dotnet nuget locals all --clear`

## Next Steps

This application provides a solid foundation for building more complex web applications with MS SQL database integration. You can extend it by:

- Adding authentication and authorization
- Implementing search and filtering
- Adding more entities and relationships
- Implementing API endpoints
- Adding logging and error handling
- Deploying to cloud platforms

## License

This project is created for educational purposes and is free to use and modify.
