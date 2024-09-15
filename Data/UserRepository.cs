using JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{
    /// <summary>
    /// Implements the IUserRepository interface, providing concrete implementations
    /// for user-related database operations using Entity Framework Core.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// </summary>
        /// <param name="context">The UserContext to be used for database operations.</param>
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Checks if a user with the specified email already exists in the database.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if a user with the email exists; otherwise, false.</returns>
        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        /// <summary>
        /// Creates a new user record in the database.
        /// </summary>
        /// <param name="user">The User object containing the user's information.</param>
        /// <returns>The created User object with its ID populated.</returns>
        /// <remarks>
        /// This method includes extensive logging for debugging purposes.
        /// In a production environment, consider using a proper logging framework.
        /// </remarks>
        public User Create(User user)
        {
            Console.WriteLine($"Before adding: User Id = {user.Id}, Email = {user.Email}");
            _context.Users.Add(user);
            Console.WriteLine($"After adding, before SaveChanges: User Id = {user.Id}, Email = {user.Email}");
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                var entries = _context.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added);
                foreach (var entry in entries)
                {
                    Console.WriteLine($"Entity: {entry.Entity.GetType().Name}");
                    foreach (var prop in entry.Properties)
                    {
                        Console.WriteLine($"  {prop.Metadata.Name}: {prop.CurrentValue}");
                    }
                }
                throw;
            }
            Console.WriteLine($"After SaveChanges: User Id = {user.Id}, Email = {user.Email}");
            return user;
        }

        /// <summary>
        /// Retrieves a user from the database based on their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The User object if found; otherwise, null.</returns>
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Retrieves a user from the database based on their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        /// <returns>The User object if found; otherwise, null.</returns>
        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}

// Usage Example:
/*
public class AuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User RegisterUser(string name, string email, string password)
    {
        if (_userRepository.EmailExists(email))
        {
            throw new Exception("Email already in use");
        }

        var user = new User { Name = name, Email = email, Password = password };
        return _userRepository.Create(user);
    }

    // Other authentication methods...
}
*/

// Notes:
// 1. This repository implements the IUserRepository interface, providing concrete database operations.
// 2. The Create method includes extensive console logging, which is useful for debugging but should be replaced with a proper logging framework in production.
// 3. Error handling in the Create method captures and logs details about failed database operations. This is helpful for diagnosing issues but may expose sensitive information if used in production.
// 4. The GetByEmail and GetById methods return null if no user is found. Consider using the null-coalescing operator (??) or throwing a custom exception for better error handling.

// Potential Improvements:
// 1. Replace console logging with a proper logging framework (e.g., Serilog, NLog).
// 2. Implement asynchronous versions of these methods for better performance in I/O-bound operations.
// 3. Add a method for updating user information.
// 4. Implement soft delete functionality if needed.
// 5. Consider using specification pattern for more complex queries.
// 6. Add pagination support for potential future methods that return multiple users.