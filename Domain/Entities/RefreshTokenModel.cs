namespace Domain.Entities
{
    public class RefreshTokenModel
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
