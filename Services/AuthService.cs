using Family_Feud.DTOs;
using Family_Feud.Models;
using Family_Feud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
// EntityFrameworkCore = database ORM
// IdentityModel = JWT token handling
// Claims = user information stored in token

namespace Family_Feud.Services
{
    public class AuthService : IAuthService
    // Implements IAuthService interface
    // Contains all authentication business logic
    // Separated from controller for testability
    {
        private readonly AppDbContext _context;
        // Database context - connection to database
        // Used for all database operations

        private readonly IConfiguration _configuration;
        // Access to appsettings.json
        // Contains JWT key, issuer, audience, expiry time

        public AuthService(AppDbContext context, IConfiguration configuration)
        // Constructor with dependency injection
        // ASP.NET provides these automatically
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        // AuthResponse? = nullable - can return null if registration fails
        // async Task = asynchronous method
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            // AnyAsync = returns true if any record matches condition
            // Equivalent SQL: SELECT COUNT(*) FROM Users WHERE Email = @email
            {
                return null;
                // Email taken - registration fails
            }

            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
                // Username taken - registration fails
            }

            // Create new user object
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                // BCrypt.HashPassword converts plain text to secure hash
                // Example: "password123" -> "$2a$11$K3x..."
                // Hash is one-way - cannot convert back to original
                // Same password = different hash each time (uses salt)
                
                Role = "Player",
                // Default role for new users
                // Only admin can change to "Admin"
                
                PreferredLanguage = request.PreferredLanguage,
                // User's chosen language ("en" or "pl")
                
                CreatedAt = DateTime.UtcNow
                // Current UTC time
                // UTC avoids timezone confusion
            };

            _context.Users.Add(user);
            // Add user to DbContext (not saved yet)
            // Entity is tracked in "Added" state

            await _context.SaveChangesAsync();
            // Execute INSERT statement in database
            // User.Id is auto-generated and populated

            // Generate JWT token for immediate login
            var token = GenerateJwtToken(user.Id, user.Username, user.Email, user.Role);

            return new AuthResponse
            {
                Token = token,
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                PreferredLanguage = user.PreferredLanguage
            };
            // Return all info frontend needs
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            // Find user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            // FirstOrDefaultAsync = return first match or null
            // Equivalent SQL: SELECT TOP 1 * FROM Users WHERE Email = @email

            if (user == null)
            {
                return null;
                // User not found - login fails
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            // Verify compares plain password with stored hash
            // Returns true if password is correct
            // Does NOT decrypt - uses same algorithm to hash input and compare
            {
                return null;
                // Wrong password - login fails
            }

            // Generate JWT token
            var token = GenerateJwtToken(user.Id, user.Username, user.Email, user.Role);

            return new AuthResponse
            {
                Token = token,
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                PreferredLanguage = user.PreferredLanguage
            };
        }

        public string GenerateJwtToken(int userId, string username, string email, string role)
        // Creates JWT token containing user information
        // Token is signed with secret key - cannot be tampered
        {
            var jwtKey = _configuration["Jwt:Key"];
            // Read secret key from appsettings.json
            // Example: "YourSuperSecretKeyThatIsAtLeast32CharactersLong..."

            var key = Encoding.ASCII.GetBytes(jwtKey!);
            // Convert string to byte array
            // JWT library requires bytes

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    // User ID - used to identify user in requests
                    // Example: "123"
                    
                    new Claim(ClaimTypes.Name, username),
                    // Username for display
                    // Example: "john_doe"
                    
                    new Claim(ClaimTypes.Email, email),
                    // User's email
                    // Example: "john@example.com"
                    
                    new Claim(ClaimTypes.Role, role)
                    // User's role - "Player" or "Admin"
                    // Used by [Authorize(Roles = "Admin")]
                }),
                // Claims = information embedded in token
                // Anyone can read claims (base64 encoded, not encrypted)
                // But cannot modify without invalidating signature

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                // Token expiration time
                // ExpiryMinutes = 1440 (24 hours)
                // After this, user must login again

                Issuer = _configuration["Jwt:Issuer"],
                // Who created the token: "FamilyFeudAPI"

                Audience = _configuration["Jwt:Audience"],
                // Who can use the token: "FamilyFeudClient"

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                // Sign token with HMAC-SHA256 algorithm
                // Signature proves token wasn't modified
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            // Handler for creating/reading JWT tokens

            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Create token object from descriptor

            return tokenHandler.WriteToken(token);
            // Convert token to string format
            // Example: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
        }
    }
}