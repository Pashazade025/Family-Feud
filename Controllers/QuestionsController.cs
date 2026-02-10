using Family_Feud.Data;
using Family_Feud.DTOs;
using Family_Feud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Family_Feud.Controllers
{
    [Route("api/[controller]")]
    // Route: /api/questions

    [ApiController]
    // API controller with automatic validation

    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        // Database context

        public QuestionsController(AppDbContext context)
        // Inject database context
        {
            _context = context;
        }

        [HttpGet]
        // GET /api/questions - retrieve all active questions
        // No [Authorize] = anyone can access (needed for gameplay)
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        // ActionResult<T> = returns T or HTTP error
        // IEnumerable<QuestionDto> = collection of questions
        {
            var questions = await _context.Questions
                .Include(q => q.Answers)
                // Include related Answers in query
                // Without this, Answers would be null
                // SQL: JOIN Answers ON Questions.Id = Answers.QuestionId

                .Where(q => q.IsActive)
                // Filter: only active questions
                // SQL: WHERE IsActive = 1

                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    QuestionTextEN = q.QuestionTextEN,
                    QuestionTextPL = q.QuestionTextPL,
                    Category = q.Category,
                    Difficulty = q.Difficulty,
                    Answers = q.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        AnswerTextEN = a.AnswerTextEN,
                        AnswerTextPL = a.AnswerTextPL,
                        Points = a.Points,
                        Rank = a.Rank
                    }).ToList()
                })
                // Select/Project = transform to DTO
                // Only include needed fields
                // Don't expose internal properties

                .ToListAsync();
            // Execute query and convert to list

            return Ok(questions);
            // Return 200 OK with JSON array
        }

        [HttpGet("{id}")]
        // GET /api/questions/5 - get single question by ID
        // {id} is route parameter
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        // int id is automatically parsed from URL
        {
            var question = await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.Id == id && q.IsActive)
                // Find by ID and must be active
                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    QuestionTextEN = q.QuestionTextEN,
                    QuestionTextPL = q.QuestionTextPL,
                    Category = q.Category,
                    Difficulty = q.Difficulty,
                    Answers = q.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        AnswerTextEN = a.AnswerTextEN,
                        AnswerTextPL = a.AnswerTextPL,
                        Points = a.Points,
                        Rank = a.Rank
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            // Get first match or null

            if (question == null)
            {
                return NotFound();
                // Return 404 if not found
            }

            return Ok(question);
        }

        [AllowAnonymous]
        // Override any [Authorize] - anyone can access
        // Used for testing, should be [Authorize(Roles = "Admin")] in production

        [HttpPost]
        // POST /api/questions - create new question
        public async Task<ActionResult<QuestionDto>> CreateQuestion([FromBody] CreateQuestionRequest request)
        // [FromBody] = deserialize JSON body to object
        {
            var userId = 1;
            // Hardcoded for now
            // Should be: int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)

            var question = new Question
            {
                QuestionTextEN = request.QuestionTextEN,
                QuestionTextPL = request.QuestionTextPL,
                Category = request.Category,
                Difficulty = request.Difficulty,
                CreatedByUserId = userId,
                IsActive = true,
                Answers = request.Answers.Select(a => new Answer
                {
                    AnswerTextEN = a.AnswerTextEN,
                    AnswerTextPL = a.AnswerTextPL,
                    Points = a.Points,
                    Rank = a.Rank
                }).ToList()
                // Create Answer entities from request
                // EF Core will automatically set QuestionId
            };

            _context.Questions.Add(question);
            // Add to context

            await _context.SaveChangesAsync();
            // Save to database
            // Question.Id is auto-generated

            var questionDto = new QuestionDto
            {
                Id = question.Id,
                QuestionTextEN = question.QuestionTextEN,
                QuestionTextPL = question.QuestionTextPL,
                Category = question.Category,
                Difficulty = question.Difficulty,
                Answers = question.Answers.Select(a => new AnswerDto
                {
                    Id = a.Id,
                    AnswerTextEN = a.AnswerTextEN,
                    AnswerTextPL = a.AnswerTextPL,
                    Points = a.Points,
                    Rank = a.Rank
                }).ToList()
            };

            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, questionDto);
            // Return 201 Created
            // Location header points to GET endpoint
            // Body contains created question
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        // DELETE /api/questions/5 - delete question
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            // FindAsync = find by primary key
            // Most efficient way to find single entity

            if (question == null)
            {
                return NotFound();
            }

            question.IsActive = false;
            // Soft delete - don't actually remove from database
            // Just mark as inactive
            // Preserves data integrity and history

            await _context.SaveChangesAsync();

            return NoContent();
            // Return 204 No Content
            // Successful delete, no body needed
        }
    }
}