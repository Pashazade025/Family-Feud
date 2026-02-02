namespace Family_Feud.Models
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string DeveloperName { get; set; } = string.Empty;
        public string DeveloperEmail { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int RateLimit { get; set; } = 100; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
