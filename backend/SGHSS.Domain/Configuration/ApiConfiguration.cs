namespace Domain.Configuration
{
    public class ApiConfiguration
    {
        public string? BaseUrl { get; set; }
        public Dictionary<string, Dictionary<string, string>>? Controllers { get; set; }
    }
}
