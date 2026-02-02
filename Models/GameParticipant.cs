using Family_Feud.Models;

namespace Family_Feud.Models
{
    public class GameParticipant
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int TeamNumber { get; set; } // 1 or 2

        // Navigation properties
        public Game? Game { get; set; }
        public User? User { get; set; }
    }
}
