using JWT.Models;

namespace JWT.Data
{
    /// <summary>
    /// Defines the contract for user-related database operations.
    /// This interface outlines the methods for creating, retrieving, and checking the existence of user records.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates a new user record in the database.
        /// </summary>
        /// <param name="user">The User object containing the user's information.</param>
        /// <returns>The created User object, typically with its ID populated.</returns>
        User Create(User user);

        /// <summary>
        /// Checks if a user with the specified email already exists in the database.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if a user with the email exists; otherwise, false.</returns>
        bool EmailExists(string email);

        /// <summary>
        /// Retrieves a user from the database based on their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The User object if found; otherwise, null or a default value.</returns>
        User GetByEmail(string email);

        /// <summary>
        /// Retrieves a user from the database based on their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        /// <returns>The User object if found; otherwise, null or a default value.</returns>
        User GetById(int id);
    }
}

// Usage Example:
/*
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User RegisterUser(User newUser)
    {
        if (_userRepository.EmailExists(newUser.Email))
        {
            throw new Exception("Email already in use");
        }
        return _userRepository.Create(newUser);
    }

    public User AuthenticateUser(string email, string password)
    {
        var user = _userRepository.GetByEmail(email);
        if (user != null && VerifyPassword(password, user.Password))
        {
            return user;
        }
        return null;
    }

    // Other methods...
}
*/

// Notes:
// 1. This interface follows the Repository pattern, abstracting data access operations.
// 2. Implement this interface in a concrete class (e.g., SqlUserRepository) for the actual database operations.
// 3. Use dependency injection to provide the concrete implementation where needed.
// 4. Consider adding methods for updating and deleting users if required by your application.
// 5. Ensure proper error handling in the implementing class, especially for database-related exceptions.

// Potential Improvements:
// 1. Add a method for retrieving multiple users (e.g., GetAll() or GetPaginated()).
// 2. Include a method for updating user information (e.g., Update(User user)).
// 3. Add a method for deleting a user (e.g., Delete(int id)).
// 4. Consider using async methods for better performance in I/O-bound operations.
// 5. Add a method for retrieving users by a specific criteria (e.g., GetByRole(string role)).