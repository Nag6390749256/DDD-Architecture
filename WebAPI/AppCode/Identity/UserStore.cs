using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Enum;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace WebAPI.AppCode.Identity
{
    public class UserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserRoleStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>, IUserLockoutStore<ApplicationUser>, IUserTwoFactorStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>, IUserAuthenticatorKeyStore<ApplicationUser>,
        IUserTwoFactorRecoveryCodeStore<ApplicationUser>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserService _userService;
        public UserStore(RoleManager<ApplicationRole> roleManager, IUserService userService)
        {
            _roleManager = roleManager;
            _userService = userService;
        }
        public async Task<IQueryable<ApplicationUser>> AllUsers()
        {
            var res = await _userService.GetAllUsers();
            return res.AsQueryable();
        }
        public IQueryable<ApplicationUser> Users => AllUsers().Result;
        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            _userService.AddToRole(user, roleName);
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var res = await _userService.CreateAsync(user);
            if (res.StatusCode == ResponseStatus.Success)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Code = "-1",
                Description = res.ResponseText
            });
        }
        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var res = await _userService.DeleteAsync(user.Id);
            if (res.StatusCode == ResponseStatus.Success)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Code = "-1",
                Description = res.ResponseText
            });
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var res = await _userService.FindByEmailAsync(normalizedEmail);
            return res.Result;
        }
        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var res = await _userService.FindByIdAsync(Convert.ToInt32(userId));
            return res.Result;
        }
        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var res = await _userService.FindByNameAsync(normalizedUserName);
            return res.Result;
        }
        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }
        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.NormalizedEmail);
        }
        public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var res = await _userService.FindByIdAsync(user.Id);
            return res.Result.NormalizedUserName;
        }
        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var list = new List<string>();
            list = await _userService.GetRolesAsync(Convert.ToInt32(user.Id));
            return list;
        }
        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var res = await _userService.FindByIdAsync(user.Id);
            return res.Result?.Id.ToString();
        }
        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.UserName);
        }
        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var res = await _userService.IsInRoleAsync(user, roleName);
            return res;
        }
        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);

        }
        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }
        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }
        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);

        }
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);

        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var res = await _userService.UpdateAsync(user, cancellationToken);
            if (res.StatusCode == ResponseStatus.Success)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError
            {
                Code = "-1",
                Description = res.ResponseText
            });
        }
        public async Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.AccessFailedCount);
        }
        public async Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.LockoutEnabled);
        }
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.LockoutEnd);
        }
        public async Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var res = await _userService.IncrementAccessFailedCountAsync(user, cancellationToken);
            return res.Result;
        }
        public async Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            await _userService.ResetAccessFailedCountAsync(user);
        }
        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            return Task.CompletedTask;
        }
        public async Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            _userService.SetLockoutEndDateAsync(user, lockoutEnd);
        }
        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                return Task.FromResult(false);
            return Task.FromResult(user.TwoFactorEnabled);
        }
        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            _userService.SetTwoFactorEnabledAsync(user, enabled);
            return Task.CompletedTask;
        }
        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new NotImplementedException();
            return Task.FromResult(user.PhoneNumber);
        }
        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new NotImplementedException();
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }
        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        private string GenrateRandom(int length)
        {
            string valid = "ABCDEFGHJKMNPQRSTUVWXYZ";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public Task<string> GetAuthenticatorKeyAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            string response = GenrateRandom(16);
            if (user != null && !string.IsNullOrEmpty(user.GAuthPin))
            {
                response = user.GAuthPin;
            }
            return Task.FromResult(response);
        }
        public Task SetAuthenticatorKeyAsync(ApplicationUser user, string key, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task<int> CountCodesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
        public Task<bool> RedeemCodeAsync(ApplicationUser user, string code, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
        public Task ReplaceCodesAsync(ApplicationUser user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
