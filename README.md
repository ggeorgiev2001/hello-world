# SQL Query Web Application

A modern C# ASP.NET Core web application for querying and managing MS SQL Server databases. This application provides both a web interface and REST API for database operations.

## Features

- **Modern Web UI**: Clean, responsive Bootstrap-based interface
- **REST API**: Complete CRUD operations with Swagger documentation
- **Advanced Querying**: Search, filter, and statistical analysis
- **Entity Framework Core**: Code-first database approach
- **Real-time Statistics**: Database analytics and reporting
- **Responsive Design**: Works on desktop and mobile devices

## Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core
- MS SQL Server / LocalDB
- Bootstrap 5.3
- Font Awesome Icons
- JavaScript (ES6+)
- Swagger/OpenAPI

## Project Structure

```
SqlQueryApp/
├── Controllers/
│   ├── ProductsController.cs    # API controller for CRUD operations
│   └── HomeController.cs        # MVC controller for web UI
├── Data/
│   └── ApplicationDbContext.cs  # Entity Framework DbContext
├── Models/
│   └── Product.cs              # Product entity model
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml      # Main layout template
│   └── Home/
│       ├── Index.cshtml        # Product listing page
│       ├── Create.cshtml       # Add new product form
│       └── Statistics.cshtml   # Database statistics
├── SqlQueryApp.csproj          # Project file with dependencies
├── Program.cs                  # Application startup and configuration
└── appsettings.json           # Configuration including connection string
```

## Prerequisites

- .NET 8.0 SDK or later
- SQL Server or SQL Server Express LocalDB
- Visual Studio 2022, VS Code, or any .NET-compatible IDE

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd SqlQueryApp
```

### 2. Configure Database Connection

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SqlQueryAppDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

For SQL Server, use:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SqlQueryAppDb;Integrated Security=true;TrustServerCertificate=true"
  }
}
```

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Run the Application

```bash
dotnet run
```

The application will start and be available at:
- **Web Interface**: https://localhost:5001 or http://localhost:5000
- **API Documentation**: https://localhost:5001/swagger

## Database Setup

The application uses Entity Framework Code-First approach. The database and tables will be created automatically when you first run the application.

### Sample Data

The application includes seed data with sample products:
- Laptop ($1,299.99)
- Smartphone ($899.99)
- Wireless Headphones ($299.99)

### Manual Database Commands (Optional)

If you need to manually manage the database:

```bash
# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Drop database (if needed)
dotnet ef database drop
```

## API Endpoints

### Products API

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products with optional filtering |
| GET | `/api/products/{id}` | Get specific product by ID |
| POST | `/api/products` | Create new product |
| PUT | `/api/products/{id}` | Update existing product |
| DELETE | `/api/products/{id}` | Delete product |
| GET | `/api/products/search-by-price-range` | Search products by price range |
| GET | `/api/products/low-stock` | Get low stock products |
| GET | `/api/products/statistics` | Get database statistics |

### Query Parameters

- `search`: Search in name and description
- `minPrice`: Minimum price filter
- `maxPrice`: Maximum price filter
- `isActive`: Filter by active status
- `threshold`: Stock threshold for low-stock query

### Example API Calls

```bash
# Get all products
curl https://localhost:5001/api/products

# Search products
curl "https://localhost:5001/api/products?search=laptop&minPrice=1000"

# Get low stock products
curl "https://localhost:5001/api/products/low-stock?threshold=10"

# Create new product
curl -X POST https://localhost:5001/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Gaming Mouse",
    "description": "High-precision gaming mouse",
    "price": 79.99,
    "stock": 30,
    "isActive": true
  }'
```

## Web Interface Features

### Product Management
- **List View**: Searchable and filterable product table
- **Create**: Add new products with validation
- **Edit**: Update existing products
- **Delete**: Remove products with confirmation
- **Details**: View complete product information

### Advanced Queries
- **Search & Filter**: Real-time search with multiple criteria
- **Statistics Dashboard**: Database analytics and metrics
- **Low Stock Alerts**: Identify products needing restocking
- **Price Range Queries**: Find products within specific price ranges

### Statistics Available
- Total number of products
- Total inventory value
- Average product price
- Minimum and maximum prices
- Total stock items
- Custom query results

## Development

### Adding New Entities

1. Create model in `Models/` folder
2. Add DbSet to `ApplicationDbContext`
3. Create controller for CRUD operations
4. Add views for web interface
5. Run migration commands

### Customizing the UI

The application uses Bootstrap 5.3 with custom CSS. Modify:
- `Views/Shared/_Layout.cshtml` for overall layout
- Individual view files for specific pages
- Add custom CSS in the layout file

### API Documentation

Visit `/swagger` when running the application to see interactive API documentation powered by Swagger/OpenAPI.

## Troubleshooting

### Common Issues

1. **Database Connection Errors**
   - Verify SQL Server is running
   - Check connection string format
   - Ensure database permissions

2. **Migration Errors**
   - Delete `Migrations/` folder and recreate
   - Check Entity Framework tools installation

3. **Port Conflicts**
   - Modify `launchSettings.json` to use different ports
   - Check firewall settings

### Database Reset

To start with a fresh database:

```bash
dotnet ef database drop --force
dotnet run
```

## Security Considerations

For production deployment, consider:
- Use secure connection strings with proper authentication
- Implement proper authorization and authentication
- Add input validation and sanitization
- Use HTTPS certificates
- Configure CORS policies appropriately
- Add rate limiting and logging

## Performance Tips

- Add database indexes for frequently queried columns
- Implement pagination for large datasets
- Use async/await patterns consistently
- Consider caching for read-heavy operations
- Monitor database query performance

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License. See LICENSE file for details.

## Support

For questions or issues:
- Check the troubleshooting section
- Review API documentation at `/swagger`
- Create an issue in the repository
