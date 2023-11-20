using Domain.Entities;

namespace Applications.Services
{
    public class UserService : Applications.Interface.IUserService
    {
        private readonly Domain.Interface.IUserService _userService;
        public UserService(Domain.Interface.IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IEnumerable<ApplicationUser>> UserList(GetUserListReq getUserListReq)
        {
            var list = await _userService.GetAllUsers(getUserListReq);
            return list;
        }
    }
}
