using Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser: IdentityUser<int>
    {
        public int BranchId { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public string GAuthPin { get; set; }
        public string Gender { get; set; }
        public string MeddleName { get; set; }
        public string LastName { get; set; }
        public string AlternateNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public int TenantId { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiryTime { get; set; }
        public ApplicationRoles? RoleId { get; set; }
        public string? EntryOn { get; set; }
        public MasterStatus Status { get; set; }
        public int CityId { get; set; }
    }
}
