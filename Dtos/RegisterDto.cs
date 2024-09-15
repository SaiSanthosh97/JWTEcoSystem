namespace JWT.Dtos
{
    /// <summary>
    /// Represents the data transfer object for user registration.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the name of the user registering.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user registering.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password for the new user account.
        /// </summary>
        public string Password { get; set; }
    }
}
// Usage Example:
/*
// Creating a registration request
var registrationRequest = new RegisterDto
{
    Name = "John Doe",
    Email = "john.doe@example.com",
    Password = "securePassword456"
};
*/