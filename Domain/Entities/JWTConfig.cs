namespace Domain.Entities
{
    public class JWTConfig
    {
        public string Secretkey { get; set; }
        public bool IsDevelopmentEnv { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInMinutes { get; set; }
        public int RefreshTokenExpiry { get; set; }
    }
}
