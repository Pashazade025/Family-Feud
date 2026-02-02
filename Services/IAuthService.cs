using Family_Feud.DTOs;

namespace Family_Feud.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        string GenerateJwtToken(int userId, string username, string email, string role);
    }
}
