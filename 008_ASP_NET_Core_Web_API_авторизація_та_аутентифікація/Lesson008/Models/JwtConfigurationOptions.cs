namespace Lesson008.Models
{
    public class JwtConfigurationOptions
    {
        public required string SecretKey { get; set; }
        public required string Audience { get; set; }
        public required string Issuer { get; set; }
    }
}
