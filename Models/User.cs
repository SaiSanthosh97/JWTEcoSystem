//# User Model Documentation

//## Overview
//This file defines the `User` model in the `JWT.Models` namespace.It represents a user entity in the system, likely used for authentication and user management purposes.

//## Class: User

//### Purpose
//The `User` class defines the structure for storing user information in the database.It includes basic user details and is designed to work with Entity Framework Core for database operations.

//### Properties

//1. **Id**
//   - Type: `int`
//   - Description: Unique identifier for the user.
//   - Annotations: 
//     - `[Key]`: Marks this as the primary key of the entity.
//     - `[DatabaseGenerated(DatabaseGeneratedOption.Identity)]`: Specifies that the database should auto-generate this value.

//2. **Name**
//   - Type: `string`
//   - Description: The name of the user.

//3. **Email**
//   - Type: `string`
//   - Description: The email address of the user.

//4. **Password**
//   - Type: `string`
//   - Description: The user's password.
//   - Annotation: `[JsonIgnore]`: Prevents this property from being serialized to JSON, enhancing security by not exposing passwords in API responses.

//## Usage Notes

//1. This model is likely used in conjunction with Entity Framework Core for database operations.
//2. The `[JsonIgnore]` attribute on the `Password` property is a security measure to prevent passwords from being exposed in API responses.
//3. Consider adding data annotations for validation (e.g., `[Required]`, `[EmailAddress]`) to ensure data integrity.

//## Dependencies

//- `System.ComponentModel.DataAnnotations`
//- `System.ComponentModel.DataAnnotations.Schema`
//- `System.Text.Json.Serialization`

//## Example Usage

//```csharp
//var newUser = new User
//{
//    Name = "John Doe",
//    Email = "john.doe@example.com",
//    Password = "hashedPassword123"
//};

//// Use this object with your data context to add to the database
//dbContext.Users.Add(newUser);
//await dbContext.SaveChangesAsync();
//```

//## Security Considerations

//1. Ensure that passwords are hashed before storing in the database.
//2. Implement additional security measures like email verification, password complexity requirements, etc.

//## Potential Improvements

//1. Add data annotations for validation (e.g., `[Required]`, `[EmailAddress]`).
//2. Consider adding fields like `CreatedAt`, `LastLoginAt` for better user management.
//3. Implement a separate `UserDto` (Data Transfer Object) for API responses to have more control over what data is exposed.
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWT.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]public string Password { get; set; }
    }
}
