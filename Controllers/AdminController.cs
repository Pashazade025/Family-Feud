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
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("users")]
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
                })
                .ToListAsync();

            return Ok(users);
        }

        
        [HttpGet("questions")]
        public async Task<ActionResult> GetAllQuestions()
        {
            var questions = await _context.Questions
                .Include(q => q.Answers)
                .Include(q => q.CreatedBy)
                .Select(q => new
                {
                    q.Id,
                    q.QuestionTextEN,
                    q.QuestionTextPL,
                    q.Category,
                    q.Difficulty,
                    q.IsActive,
                    CreatedBy = q.CreatedBy!.Username,
                    AnswersCount = q.Answers.Count
                })
                .ToListAsync();

            return Ok(questions);
        }

        
        [HttpDelete("questions/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            question.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpPost("apikeys")]
        public async Task<ActionResult<ApiKeyResponse>> GenerateApiKey([FromBody] ApiKeyRequest request)
        {
           
            var existingKey = await _context.ApiKeys
                .FirstOrDefaultAsync(k => k.DeveloperEmail == request.DeveloperEmail);

            if (existingKey != null)
            {
                return BadRequest(new { message = "This email already has an API key" });
            }

            
            var apiKey = GenerateRandomApiKey();

            var newApiKey = new ApiKey
            {
                Key = apiKey,
                DeveloperName = request.DeveloperName,
                DeveloperEmail = request.DeveloperEmail,
                IsActive = true,
                RateLimit = 100,
                CreatedAt = DateTime.UtcNow
            };

            _context.ApiKeys.Add(newApiKey);
            await _context.SaveChangesAsync();

            return Ok(new ApiKeyResponse
            {
                ApiKey = apiKey,
                Message = "API key generated successfully. Please save it securely - you won't be able to see it again."
            });
        }

        
        [HttpGet("apikeys")]
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
                })
                .ToListAsync();

            return Ok(apiKeys);
        }

        
        [HttpDelete("apikeys/{id}")]
        public async Task<IActionResult> RevokeApiKey(int id)
        {
            var apiKey = await _context.ApiKeys.FindAsync(id);
            if (apiKey == null)
            {
                return NotFound();
            }

            apiKey.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateRoleRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (request.Role != "Player" && request.Role != "Admin")
            {
                return BadRequest(new { message = "Role must be 'Player' or 'Admin'" });
            }

            user.Role = request.Role;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpGet("stats")]
        public async Task<ActionResult> GetStatistics()
        {
            var stats = new
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalQuestions = await _context.Questions.Where(q => q.IsActive).CountAsync(),
                TotalGames = await _context.Games.CountAsync(),
                ActiveGames = await _context.Games.Where(g => g.Status == "InProgress").CountAsync(),
                CompletedGames = await _context.Games.Where(g => g.Status == "Completed").CountAsync(),
                TotalApiKeys = await _context.ApiKeys.Where(k => k.IsActive).CountAsync()
            };

            return Ok(stats);
        }

        
        private string GenerateRandomApiKey()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
            }

            var result = new char[32];
            for (int i = 0; i < 32; i++)
            {
                result[i] = chars[random[i] % chars.Length];
            }

            return "ff_" + new string(result); 
        }
    }

    public class UpdateRoleRequest
    {
        public string Role { get; set; } = string.Empty;
    }
}
