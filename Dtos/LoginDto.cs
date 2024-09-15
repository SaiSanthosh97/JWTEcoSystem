namespace JWT.Dtos
{
    /// <summary>
    /// Represents the data transfer object for user login.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the email address of the user attempting to log in.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user attempting to log in.
        /// </summary>
        public string Password { get; set; }
    }
}

// Usage Example:
/*
// Creating a login request
var loginRequest = new LoginDto
{
    Email = "user@example.com",
    Password = "userPassword123"
};
*/