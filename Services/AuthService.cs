using Family_Feud.DTOs;
using Family_Feud.Models;
using Family_Feud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;




namespace Family_Feud.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return null; 
            }

            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null; 
            }

            
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "Player",
                PreferredLanguage = request.PreferredLanguage,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
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

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return null; 
            }

            
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null; 
            }

            
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
        {
            var jwtKey = _configuration["Jwt:Key"];
            var key = Encoding.ASCII.GetBytes(jwtKey!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
