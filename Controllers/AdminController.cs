using Family_Feud.Data;
using Family_Feud.DTOs;
using Family_Feud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Family_Feud.Controllers
{
    [Route("api/[controller]")]
    // Route: /api/admin

    [ApiController]

    [Authorize(Roles = "Admin")]
    // ENTIRE controller requires Admin role
    // Applied to all methods in this controller
    // Non-admin users get 403 Forbidden

    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        // GET /api/admin/users - list all users
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Email,
                    u.Role,
                    u.PreferredLanguage,
                    u.CreatedAt
                    // Note: PasswordHash is NOT included
                    // Never expose passwords, even hashed ones
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("questions")]
        // GET /api/admin/questions - list all questions with details
        public async Task<ActionResult> GetAllQuestions()
        {
            var questions = await _context.Questions
                .Include(q => q.Answers)
                // Include answers for count

                .Include(q => q.CreatedBy)
                // Include creator user for username

                .Select(q => new
                {
                    q.Id,
                    q.QuestionTextEN,
                    q.QuestionTextPL,
                    q.Category,
                    q.Difficulty,
                    q.IsActive,
                    CreatedBy = q.CreatedBy!.Username,
                    // Show who created the question

                    AnswersCount = q.Answers.Count
                    // Number of answers for this question
                })
                .ToListAsync();

            return Ok(questions);
        }

        [HttpDelete("questions/{id}")]
        // DELETE /api/admin/questions/5 - soft delete question
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            question.IsActive = false;
            // Soft delete - keep data but hide from game

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("apikeys")]
        // POST /api/admin/apikeys - generate new API key for developer
        public async Task<ActionResult<ApiKeyResponse>> GenerateApiKey([FromBody] ApiKeyRequest request)
        {
            // Check if developer already has a key
            var existingKey = await _context.ApiKeys
                .FirstOrDefaultAsync(k => k.DeveloperEmail == request.DeveloperEmail);

            if (existingKey != null)
            {
                return BadRequest(new { message = "This email already has an API key" });
                // One key per developer email
            }

            // Generate random API key
            var apiKey = GenerateRandomApiKey();
            // Example: "ff_Ab3Cd5Ef7Gh9Ij1Kl3Mn5Op7Qr9St1Uv"

            var newApiKey = new ApiKey
            {
                Key = apiKey,
                DeveloperName = request.DeveloperName,
                DeveloperEmail = request.DeveloperEmail,
                IsActive = true,
                RateLimit = 100,
                // Allow 100 requests per hour

                CreatedAt = DateTime.UtcNow
            };

            _context.ApiKeys.Add(newApiKey);
            await _context.SaveChangesAsync();

            return Ok(new ApiKeyResponse
            {
                ApiKey = apiKey,
                // Return FULL key only this once
                // After this, only preview is shown

                Message = "API key generated successfully. Please save it securely - you won't be able to see it again."
            });
        }

        [HttpGet("apikeys")]
        // GET /api/admin/apikeys - list all API keys
        public async Task<ActionResult> GetAllApiKeys()
        {
            var apiKeys = await _context.ApiKeys
                .Select(k => new
                {
                    k.Id,
                    k.DeveloperName,
                    k.DeveloperEmail,
                    k.IsActive,
                    k.RateLimit,
                    k.CreatedAt,
                    KeyPreview = k.Key.Substring(0, 8) + "..."
                    // Only show first 8 characters for security
                    // Full key never shown again after creation
                })
                .ToListAsync();

            return Ok(apiKeys);
        }

        [HttpDelete("apikeys/{id}")]
        // DELETE /api/admin/apikeys/5 - revoke API key
        public async Task<IActionResult> RevokeApiKey(int id)
        {
            var apiKey = await _context.ApiKeys.FindAsync(id);
            if (apiKey == null)
            {
                return NotFound();
            }

            apiKey.IsActive = false;
            // Deactivate, don't delete
            // Keeps audit trail of who had access

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("users/{id}/role")]
        // PUT /api/admin/users/5/role - change user's role
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateRoleRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Validate role value
            if (request.Role != "Player" && request.Role != "Admin")
            {
                return BadRequest(new { message = "Role must be 'Player' or 'Admin'" });
                // Only allow valid roles
            }

            user.Role = request.Role;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("stats")]
        // GET /api/admin/stats - dashboard statistics
        public async Task<ActionResult> GetStatistics()
        {
            var stats = new
            {
                TotalUsers = await _context.Users.CountAsync(),
                // Count all users

                TotalQuestions = await _context.Questions.Where(q => q.IsActive).CountAsync(),
                // Count only active questions

                TotalGames = await _context.Games.CountAsync(),
                // Count all games ever played

                ActiveGames = await _context.Games.Where(g => g.Status == "InProgress").CountAsync(),
                // Count games currently being played

                CompletedGames = await _context.Games.Where(g => g.Status == "Completed").CountAsync(),
                // Count finished games

                TotalApiKeys = await _context.ApiKeys.Where(k => k.IsActive).CountAsync()
                // Count active API keys
            };

            return Ok(stats);
        }

        private string GenerateRandomApiKey()
        // Generate secure random API key
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            // Characters to use in key

            var random = new byte[32];
            // 32 random bytes

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                // Fill array with cryptographically secure random bytes
                // More secure than Random class
            }

            var result = new char[32];
            for (int i = 0; i < 32; i++)
            {
                result[i] = chars[random[i] % chars.Length];
                // Convert each byte to a character
            }

            return "ff_" + new string(result);
            // Prefix with "ff_" (family feud)
            // Makes keys easily identifiable
            // Example: "ff_Ab3Cd5Ef7Gh9Ij1Kl3Mn5Op7Qr9St1Uv"
        }
    }

    public class UpdateRoleRequest
    // DTO for role update request
    {
        public string Role { get; set; } = string.Empty;
        // Expected values: "Player" or "Admin"
    }
}