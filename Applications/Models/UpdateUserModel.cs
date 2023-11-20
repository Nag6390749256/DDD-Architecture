namespace Applications.Models
{
    public class UpdateUserModel
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MeddleName { get; set; }
        public string AlternateNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
    }
}
