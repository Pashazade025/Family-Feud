using Family_Feud.Models;
using Microsoft.EntityFrameworkCore;

namespace Family_Feud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameParticipant> GameParticipants { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@familyfeud.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin",
                    PreferredLanguage = "en",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed sample questions
            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    QuestionTextEN = "Name something people bring to the beach",
                    QuestionTextPL = "Wymień coś, co ludzie zabierają na plażę",
                    Category = "General",
                    Difficulty = 1,
                    IsActive = true,
                    CreatedByUserId = 1
                },
                new Question
                {
                    Id = 2,
                    QuestionTextEN = "Name a popular pet",
                    QuestionTextPL = "Wymień popularne zwierzę domowe",
                    Category = "Animals",
                    Difficulty = 1,
                    IsActive = true,
                    CreatedByUserId = 1
                }
            );

            // Seed answers for question 1
            modelBuilder.Entity<Answer>().HasData(
                new Answer { Id = 1, QuestionId = 1, AnswerTextEN = "Towel", AnswerTextPL = "Ręcznik", Points = 45, Rank = 1 },
                new Answer { Id = 2, QuestionId = 1, AnswerTextEN = "Sunscreen", AnswerTextPL = "Krem z filtrem", Points = 25, Rank = 2 },
                new Answer { Id = 3, QuestionId = 1, AnswerTextEN = "Umbrella", AnswerTextPL = "Parasol", Points = 15, Rank = 3 },
                new Answer { Id = 4, QuestionId = 1, AnswerTextEN = "Water", AnswerTextPL = "Woda", Points = 10, Rank = 4 },
                new Answer { Id = 5, QuestionId = 1, AnswerTextEN = "Snacks", AnswerTextPL = "Przekąski", Points = 5, Rank = 5 }
            );

            // Seed answers for question 2
            modelBuilder.Entity<Answer>().HasData(
                new Answer { Id = 6, QuestionId = 2, AnswerTextEN = "Dog", AnswerTextPL = "Pies", Points = 50, Rank = 1 },
                new Answer { Id = 7, QuestionId = 2, AnswerTextEN = "Cat", AnswerTextPL = "Kot", Points = 35, Rank = 2 },
                new Answer { Id = 8, QuestionId = 2, AnswerTextEN = "Fish", AnswerTextPL = "Ryba", Points = 10, Rank = 3 },
                new Answer { Id = 9, QuestionId = 2, AnswerTextEN = "Bird", AnswerTextPL = "Ptak", Points = 5, Rank = 4 }
            );
        }
    }
}
