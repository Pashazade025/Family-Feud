namespace Family_Feud.DTOs
{
    public class ApiKeyRequest
    {
        public string DeveloperName { get; set; } = string.Empty;
        public string DeveloperEmail { get; set; } = string.Empty;
    }

    public class ApiKeyResponse
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
