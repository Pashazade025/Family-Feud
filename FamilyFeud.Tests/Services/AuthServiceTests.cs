using Family_Feud.DTOs;
using Family_Feud.Services;
using FamilyFeud.Tests.Helpers;
using FamilyFeud.Tests.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.CodeCoverage;
using Xunit;

namespace FamilyFeud.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly IConfiguration _configuration;

        public AuthServiceTests()
        {
            
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Jwt:Key", "YourSuperSecretKeyThatIsAtLeast32CharactersLongForSecurity!"},
                {"Jwt:Issuer", "FamilyFeudAPI"},
                {"Jwt:Audience", "FamilyFeudClient"},
                {"Jwt:ExpiryMinutes", "60"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();
        }

        [Fact]
        public async Task RegisterAsync_WithValidData_ReturnsAuthResponse()
        {
            
            var context = TestDbContextFactory.CreateInMemoryContext();
            var authService = new AuthService(context, _configuration);

            var registerRequest = new RegisterRequest
            {
                Username = "newuser",
                Email = "newuser@test.com",
                Password = "NewUser123!",
                PreferredLanguage = "en"
            };

            
            var result = await authService.RegisterAsync(registerRequest);

            
            Assert.NotNull(result);
            Assert.Equal("newuser", result.Username);
            Assert.Equal("newuser@test.com", result.Email);
            Assert.NotNull(result.Token);
            Assert.NotEmpty(result.Token);
        }

        [Fact]
        public async Task RegisterAsync_WithDuplicateEmail_ReturnsNull()
        {
            
            var context = TestDbContextFactory.CreateInMemoryContext();
            var authService = new AuthService(context, _configuration);

            var registerRequest = new RegisterRequest
            {
                Username = "anotheruser",
                Email = "test@test.com", 
                Password = "Test123!",
                PreferredLanguage = "en"
            };

            
            var result = await authService.RegisterAsync(registerRequest);

            
            Assert.Null(result);
        }

        [Fact]
        public async Task RegisterAsync_WithDuplicateUsername_ReturnsNull()
        {
            
            var context = TestDbContextFactory.CreateInMemoryContext();
            var authService = new AuthService(context, _configuration);

            var registerRequest = new RegisterRequest
            {
                Username = "testuser", 
                Email = "different@test.com",
                Password = "Test123!",
                PreferredLanguage = "en"
            };

            
            var result = await authService.RegisterAsync(registerRequest);

            
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_WithValidCredentials_ReturnsAuthResponse()
        {
            
            var context = TestDbContextFactory.CreateInMemoryContext();
            var authService = new AuthService(context, _configuration);

            var loginRequest = new LoginRequest
            {
                Email = "test@test.com",
                Password = "Test123!"
            };

            
            var result = await authService.LoginAsync(loginRequest);

            
            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async Task LoginAsync_WithInvalidEmail_ReturnsNull()
        {
            
            var context = TestDbContextFactory.CreateInMemoryContext();
            var authService = new AuthService(context, _configuration);

            var loginRequest = new LoginRequest
            {
                Email = "nonexistent@test.com",
                Password = "Test123!"
            };

            
            var result = await authService.LoginAsync(loginRequest);

            
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_WithInvalidPassword_ReturnsNull()
        {
            
            var context = TestDbContextFactory.CreateInMemoryContext();
            var authService = new AuthService(context, _configuration);

            var loginRequest = new LoginRequest
            {
                Email = "test@test.com",
                Password = "WrongPassword123!"
            };

            
            var result = await authService.LoginAsync(loginRequest);

            
            Assert.Null(result);
        }

        [Fact]
        public void GenerateJwtToken_ReturnsValidToken()
        {
            
            var context = TestDbContextFactory.CreateInMemoryContext();
            var authService = new AuthService(context, _configuration);

            
            var token = authService.GenerateJwtToken(1, "testuser", "test@test.com", "Player");

            
            Assert.NotNull(token);
            Assert.NotEmpty(token);
            Assert.Contains(".", token); 
        }

        [Fact]
        public async Task RegisterAsync_CreatesUserWithHashedPassword()
        {
            
            var context = TestDbContextFactory.CreateInMemoryContext();
            var authService = new AuthService(context, _configuration);

            var registerRequest = new RegisterRequest
            {
                Username = "secureuser",
                Email = "secure@test.com",
                Password = "SecurePass123!",
                PreferredLanguage = "en"
            };

           
            var result = await authService.RegisterAsync(registerRequest);

            
            var user = context.Users.FirstOrDefault(u => u.Email == "secure@test.com");
            Assert.NotNull(user);
            Assert.NotEqual("SecurePass123!", user.PasswordHash); 
            Assert.True(BCrypt.Net.BCrypt.Verify("SecurePass123!", user.PasswordHash)); 
        }
    }
}
