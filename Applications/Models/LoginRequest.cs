using System.ComponentModel.DataAnnotations;

namespace Applications.Models
{
    public class LoginRequest
    {
        [Required]
        public string EmailOrMobile { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
