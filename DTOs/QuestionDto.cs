namespace Family_Feud.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionTextEN { get; set; } = string.Empty;
        public string QuestionTextPL { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Difficulty { get; set; }
        public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
    }

    public class AnswerDto
    {
        public int Id { get; set; }
        public string AnswerTextEN { get; set; } = string.Empty;
        public string AnswerTextPL { get; set; } = string.Empty;
        public int Points { get; set; }
        public int Rank { get; set; }
        public bool IsRevealed { get; set; } = false;
    }
}
