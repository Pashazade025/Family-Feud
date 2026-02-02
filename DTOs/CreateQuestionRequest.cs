namespace Family_Feud.DTOs
{
    public class CreateQuestionRequest
    {
        public string QuestionTextEN { get; set; } = string.Empty;
        public string QuestionTextPL { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 1;
        public List<CreateAnswerRequest> Answers { get; set; } = new List<CreateAnswerRequest>();
    }

    public class CreateAnswerRequest
    {
        public string AnswerTextEN { get; set; } = string.Empty;
        public string AnswerTextPL { get; set; } = string.Empty;
        public int Points { get; set; }
        public int Rank { get; set; }
    }
}
