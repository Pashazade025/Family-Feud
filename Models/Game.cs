using Family_Feud.Models;

namespace Family_Feud.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int HostUserId { get; set; }
        public string Status { get; set; } = "Waiting"; // Waiting, InProgress, Completed
        public int? CurrentQuestionId { get; set; }
        public int Team1Score { get; set; } = 0;
        public int Team2Score { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User? Host { get; set; }
        public Question? CurrentQuestion { get; set; }
        public List<GameParticipant> Participants { get; set; } = new List<GameParticipant>();
    }
}
