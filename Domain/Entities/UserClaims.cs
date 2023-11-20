namespace Domain.Entities
{
    public class UserClaims:Base
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int UserId { get; set; }
    }
}
