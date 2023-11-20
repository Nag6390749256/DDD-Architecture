using Domain.Entities;
using Domain.Interface;
using Infrastructure.Enum;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.AppCode.Identity
{
    public class RoleStore : IRoleStore<ApplicationRole>, IQueryableRoleStore<ApplicationRole>
    {
        private readonly IUserService _userService;
        public RoleStore(IUserService userService)
        {
            _userService = userService;
        }
        public IQueryable<ApplicationRole> Roles => AllRolesAsync().Result;

        public async Task<IQueryable<ApplicationRole>> AllRolesAsync()
        {
            var roleList = new List<ApplicationRole>();
            var allRolesResponse = await _userService.GetAllRolesAsync();
            if (allRolesResponse.StatusCode == ResponseStatus.Success)
            {
                var allRoles = allRolesResponse.Result;
                return allRoles.AsQueryable();
            }
            return roleList.AsQueryable();
        }

        public Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           GC.SuppressFinalize(this);
        }

        public Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
