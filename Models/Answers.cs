namespace Family_Feud.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerTextEN { get; set; } = string.Empty;
        public string AnswerTextPL { get; set; } = string.Empty;
        public int Points { get; set; } // 1-100
        public int Rank { get; set; } // 1-8 (most popular = 1)

        
        public Question? Question { get; set; }
    }
}
