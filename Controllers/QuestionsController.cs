using Family_Feud.Data;
using Family_Feud.DTOs;
using Family_Feud.Models;
using Family_Feud.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Family_Feud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var questions = await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.IsActive)
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
                .ToListAsync();

            return Ok(questions);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            var question = await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.Id == id && q.IsActive)
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

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion([FromBody] CreateQuestionRequest request)
        {
            var userId = 1; 

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
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

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
        }


        [AllowAnonymous]
        [HttpDelete("{id}")]
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
    }
}
