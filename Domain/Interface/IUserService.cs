using Domain.Entities;
using Infrastructure;

namespace Domain.Interface
{
    public interface IUserService
    {
        Task<Response> CreateAsync(ApplicationUser users);
        Task<Response> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<Response> DeleteAsync(int userId);
        Task<Response<ApplicationUser>> FindByEmailAsync(string normalizedEmail);
        Task<Response<ApplicationUser>> FindByIdAsync(int userId);
        Task<Response<ApplicationUser>> FindByNameAsync(string normalizedUserName);
        Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);
        Task<Response<ApplicationUser>> FindByMobileAsync(string mobileNo);
        Task<Response<int>> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<Response<IEnumerable<ApplicationRole>>> GetAllRolesAsync();
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<IEnumerable<ApplicationUser>> GetAllUsers(GetUserListReq getUserListReq);
        Task AddToRole(ApplicationUser user, string roleName);
        Task ResetAccessFailedCountAsync(ApplicationUser user);
        Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd);
        Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled);
        Task<List<string>> GetRolesAsync(int userId);
    }
}
