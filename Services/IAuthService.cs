using Family_Feud.DTOs;

namespace Family_Feud.Services
{
    public interface IAuthService
    // Interface = contract that defines what methods a class must have
    // AuthService implements this interface
    // Allows dependency injection and easy testing (can mock this)
    {
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        // Method signature for registration
        // Returns AuthResponse or null

        Task<AuthResponse?> LoginAsync(LoginRequest request);
        // Method signature for login
        // Returns AuthResponse or null

        string GenerateJwtToken(int userId, string username, string email, string role);
        // Method signature for token generation
        // Returns JWT token string
    }
}