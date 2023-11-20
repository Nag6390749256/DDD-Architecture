using Domain.Enum;

namespace Domain.Entities
{
    public class GetUserListReq
    {
        public int BranchId { get; set; }
        public ApplicationRoles RoleId { get; set; }
        public int WID { get; set; }
    }
}
