using Family_Feud.Models;

namespace Family_Feud.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionTextEN { get; set; } = string.Empty;
        public string QuestionTextPL { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public int CreatedByUserId { get; set; }

        public User? CreatedBy { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
