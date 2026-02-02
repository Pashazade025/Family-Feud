using Microsoft.EntityFrameworkCore;
using Family_Feud.Data;

namespace FamilyFeud.Tests.Helpers
{
    public static class TestDbContextFactory
    {
        public static AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            // Seed test data
            SeedTestData(context);

            return context;
        }

        private static void SeedTestData(AppDbContext context)
        {
            // Add test user
            var testUser = new Family_Feud.Models.User
            {
                Id = 1,
                Username = "testuser",
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123!"),
                Role = "Player",
                PreferredLanguage = "en",
                CreatedAt = DateTime.UtcNow
            };

            context.Users.Add(testUser);
            context.SaveChanges();
        }
    }
}