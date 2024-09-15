using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JWT.Helpers
{
    /// <summary>
    /// Provides services for generating and verifying JSON Web Tokens (JWTs).
    /// </summary>
    public class JwtService
    {
        /// <summary>
        /// The secret key used for signing and verifying JWTs.
        /// </summary>
        /// <remarks>
        /// WARNING: This should be stored securely (e.g., in configuration) and not hard-coded.
        /// Ensure it's at least 32 characters long for security.
        /// </remarks>
        private string secureKey = "your-secure-key-here-make-it-at-least-32-characters-long";

        /// <summary>
        /// Generates a JWT for a given user ID.
        /// </summary>
        /// <param name="id">The user ID to be included in the JWT payload.</param>
        /// <returns>A string representing the generated JWT.</returns>
        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, algorithm:SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var paylod = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, paylod);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        /// <summary>
        /// Verifies a JWT and returns the decoded token.
        /// </summary>
        /// <param name="jwt">The JWT string to verify.</param>
        /// <returns>A JwtSecurityToken object representing the verified and decoded JWT.</returns>
        /// <exception cref="SecurityTokenException">Thrown when the token is invalid.</exception>
        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
    }
}
// Usage Example:
/*
var jwtService = new JwtService();

// Generate a token
int userId = 123;
string token = jwtService.Generate(userId);
Console.WriteLine($"Generated Token: {token}");

// Verify a token
try
{
    var decodedToken = jwtService.Verify(token);
    Console.WriteLine($"Token is valid. User ID: {decodedToken.Subject}");
}
catch (SecurityTokenException)
{
    Console.WriteLine("Token is invalid.");
}
*/

// Security Considerations:
// 1. Store the secureKey in a configuration file or secret management system, not in code.
// 2. Use a strong, unique key for each environment (development, staging, production).
// 3. Consider using asymmetric keys (RSA) for better security in distributed systems.
// 4. Implement token revocation mechanism if needed.
// 5. Be cautious about token expiration times. Short-lived tokens are generally safer.

// Potential Improvements:
// 1. Add more claims to the token payload (e.g., username, roles).
// 2. Implement refresh token mechanism for long-term authentication.
// 3. Add logging for token generation and verification attempts.
// 4. Use dependency injection to provide the secure key and other configurations.