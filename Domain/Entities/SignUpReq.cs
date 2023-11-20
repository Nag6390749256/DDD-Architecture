using Domain.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SignUpReq
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string AlternateNo { get; set; }
        [Required]
        public string WhatsappNo { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PostalCode { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public ApplicationRoles RoleId { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password must contain only alphanumeric characters.")]
        public string Password { get; set; }
    }
}
