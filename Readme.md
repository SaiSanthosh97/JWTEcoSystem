# JWT Authentication Project

## Overview

This project is a .NET 8 Web API that demonstrates JSON Web Token (JWT) authentication. It provides a robust architecture for user management and authentication, showcasing best practices in .NET development and security.

## Architecture

The project follows a layered architecture, separating concerns into distinct areas:

1. **Presentation Layer**: API Controllers
   - Located in the `Controllers` folder
   - Handles HTTP requests and responses
   - Uses DTOs for data transfer

2. **Business Logic Layer**: Services
   - Includes the `JwtService` in the `Helpers` folder
   - Manages core business logic like JWT generation and verification

3. **Data Access Layer**: Repositories and DbContext
   - Uses the Repository pattern (`IUserRepository` and `UserRepository`)
   - Utilizes Entity Framework Core with `UserContext`

4. **Domain Model**: Entities
   - The `User` model in the `Models` folder

## Design Patterns

1. **Repository Pattern**: Implemented through `IUserRepository` and `UserRepository`, providing a abstraction layer between the business logic and data access layers.

2. **Dependency Injection**: Used throughout the application, as seen in the `Program.cs` file where services are registered.

3. **DTO (Data Transfer Object) Pattern**: Used for API requests and responses, separating the internal domain model from the API contract.

## Project Structure

```
JWT/
│
├── Controllers/
│   └── Auth.cs
│
├── Data/
│   ├── IUserRepository.cs
│   ├── UserRepository.cs
│   └── UserContext.cs
│
├── Dtos/
│   ├── LoginDto.cs
│   └── RegisterDto.cs
│
├── Helpers/
│   └── JwtService.cs
│
├── Models/
│   └── User.cs
│
├── Program.cs
├── appsettings.json
└── JWT.csproj
```

## Technologies Used

- .NET 8
- Entity Framework Core 8
- SQL Server
- BCrypt.Net-Next for password hashing
- System.IdentityModel.Tokens.Jwt for JWT handling
- Swashbuckle for Swagger/OpenAPI support

## Setup and Installation

1. Clone the repository
2. Ensure you have .NET 8 SDK installed
3. Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "Default": "Your_SQL_Server_Connection_String_Here"
     }
   }
   ```
4. Open a terminal in the project directory and run:
   ```
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

## Database Migrations

This project uses Entity Framework Core migrations. To manage your database schema:

- Create a new migration:
  ```
  dotnet ef migrations add MigrationName
  ```
- Apply migrations to the database:
  ```
  dotnet ef database update
  ```
- Remove the last migration:
  ```
  dotnet ef migrations remove
  ```

## API Endpoints

- POST `/api/register`: Register a new user
- POST `/api/login`: Authenticate a user and receive a JWT
- GET `/api/user`: Get the current user's information (requires authentication)
- POST `/api/logout`: Log out the current user

## CORS Configuration

CORS is configured in `Program.cs` to allow requests from:
- http://localhost:3000
- http://localhost:8080
- http://localhost:4200

Modify these in `Program.cs` if needed for your frontend applications.

## Security Features

1. Password Hashing: BCrypt is used for secure password storage
2. JWT Authentication: Tokens are stored in HTTP-only cookies
3. HTTPS Redirection: Enforced for all communications
4. Secure Cookie Handling: Prevents client-side script access to authentication tokens

## Development Tools

- Swagger UI: Available at `/swagger` when running in development mode
- Entity Framework Core Tools: For managing database migrations

## Key Code Elements

1. **UserContext (Data/UserContext.cs)**:
   - Configures the database schema
   - Sets up unique constraints on the email field

2. **UserRepository (Data/UserRepository.cs)**:
   - Implements CRUD operations for User entities
   - Includes error logging for debugging database operations

3. **Auth Controller (Controllers/Auth.cs)**:
   - Manages user authentication flow
   - Implements JWT token generation and verification

4. **JwtService (Helpers/JwtService.cs)**:
   - Handles JWT token generation and validation

5. **Program.cs**:
   - Configures services and middleware
   - Sets up dependency injection
   - Configures CORS and Swagger

## Best Practices Demonstrated

- Separation of Concerns: Clear separation between controllers, services, and data access
- Security: Use of JWT, secure password hashing, and HTTPS
- Scalability: Repository pattern allows for easy switching of data sources
- API Documentation: Swagger integration for easy API testing and documentation

## Contributing

Contributions are welcome. Please adhere to the existing architectural patterns and coding style. Add unit tests for new features.

## License

[Specify your license here]