using JWT.Data;
using JWT.Dtos;
using JWT.Helpers;
using JWT.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    /// <summary>
    /// Handles authentication-related operations including user registration, login, user information retrieval, and logout.
    /// </summary>
    [Route(template: "api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        /// <summary>
        /// Initializes a new instance of the Auth controller.
        /// </summary>
        /// <param name="repository">The user repository for database operations.</param>
        /// <param name="jwtService">The JWT service for token generation and verification.</param>
        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _userRepository = repository;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="dto">The registration data transfer object containing user details.</param>
        /// <returns>A response indicating success or failure of the registration.</returns>
        [HttpPost(template: "register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (_userRepository.EmailExists(dto.Email))
            {
                return BadRequest(new { message = "Email already exists" });
            }
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            Console.WriteLine($"In Register method: Email = {user.Email}");
            var createdUser = _userRepository.Create(user);
            return Created(uri: "Success", value: createdUser);
        }

        /// <summary>
        /// Authenticates a user and issues a JWT token.
        /// </summary>
        /// <param name="dto">The login data transfer object containing user credentials.</param>
        /// <returns>A response indicating success or failure of the login attempt.</returns>
        [HttpPost(template: "login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _userRepository.GetByEmail(dto.Email);
            if (user == null) return BadRequest(error: new { message = "Invalid Credentials" });
            if (!BCrypt.Net.BCrypt.Verify(text: dto.Password, hash: user.Password))
            {
                return BadRequest(error: new { message = "Invalid Credentials" });
            }
            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });
            return Ok(new { message = "success" });
        }

        /// <summary>
        /// Retrieves the authenticated user's information.
        /// </summary>
        /// <returns>The user information if authenticated, or an Unauthorized response.</returns>
        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _userRepository.GetById(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Logs out the current user by removing the JWT cookie.
        /// </summary>
        /// <returns>A response indicating successful logout.</returns>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "success" });
        }
    }
}

// Usage Notes:
// 1. This controller handles the main authentication flow: registration, login, user info retrieval, and logout.
// 2. JWT tokens are used for maintaining user sessions. The token is stored in an HTTP-only cookie for security.
// 3. Passwords are hashed using BCrypt before storage.
// 4. The 'user' endpoint requires a valid JWT token in the request cookies to return user information.

// Security Considerations:
// 1. Ensure that all endpoints are served over HTTPS to protect sensitive information.
// 2. The JWT cookie is set as HttpOnly, which is a good security practice to prevent XSS attacks.
// 3. Consider implementing rate limiting to prevent brute-force attacks on the login endpoint.
// 4. Implement proper error handling and logging, especially for the User() method where exceptions are caught.

// Potential Improvements:
// 1. Implement refresh token functionality for better security and user experience.
// 2. Add input validation attributes to the DTOs to ensure data integrity.
// 3. Consider using ASP.NET Core Identity for more robust authentication and authorization features.
// 4. Implement two-factor authentication for enhanced security.
// 5. Add CSRF protection, especially for state-changing operations like logout.
// 6. Use a more specific exception type in the User() method instead of catching all exceptions.
// 7. Consider returning more specific HTTP status codes (e.g., 404 Not Found instead of 400 Bad Request for invalid credentials).