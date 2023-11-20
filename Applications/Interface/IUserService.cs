using Domain.Entities;

namespace Applications.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> UserList(GetUserListReq getUserListReq);
    }
}
