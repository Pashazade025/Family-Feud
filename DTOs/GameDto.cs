namespace Family_Feud.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public QuestionDto? CurrentQuestion { get; set; }
        public string HostUsername { get; set; } = string.Empty;
        public List<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
    }

    public class ParticipantDto
    {
        public string Username { get; set; } = string.Empty;
        public int TeamNumber { get; set; }
    }
}
