using Domain.Entities;
using Domain.Interface;
using Infrastructure;
using Infrastructure.Enum;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IDapperRepository dapper, ILogger<UserService> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }
        public async Task<Response> CreateAsync(ApplicationUser users)
        {
            var response = new Response { ResponseText = "Failed to create user!" };
            try
            {
                response = await _dapper.GetAsync<Response>("Proc_AddUser", new
                {
                    users.Id,
                    UserId = Guid.NewGuid().ToString(),
                    users.SecurityStamp,
                    users.PhoneNumber,
                    users.PhoneNumberConfirmed,
                    users.PasswordHash,
                    users.NormalizedUserName,
                    users.NormalizedEmail,
                    users.LockoutEnd,
                    users.LockoutEnabled,
                    users.EmailConfirmed,
                    users.Email,
                    users.ConcurrencyStamp,
                    users.AccessFailedCount,
                    users.TwoFactorEnabled,
                    users.UserName,
                    users.GAuthPin,
                    users.RefreshToken,
                    users.RefreshTokenExpiryTime,
                    users.Gender,
                    users.TenantId,
                    users.FirstName,
                    users.MeddleName,
                    users.LastName,
                    users.AlternateNumber,
                    users.WhatsAppNumber,
                    users.Address,
                    users.PostalCode,
                    users.BranchId,
                }, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseText = "Something went wrong try after sometime!";
            }
            return response;
        }
        public async Task<Response> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var response = new Response { ResponseText = "Failed to update user!" };
            try
            {
                response = await _dapper.GetAsync<Response>("UpdateUser", new
                {
                    user.Id,
                    user.FirstName,
                    user.MeddleName,
                    user.LastName,
                    user.Email,
                    user.PostalCode,
                    user.Address,
                    user.AlternateNumber,
                    user.PhoneNumber,
                    user.WhatsAppNumber,
                    user.Gender,
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseText = "Something went wrong try after sometime!";
            }
            return response;
        }
        public async Task<Response> DeleteAsync(int userId)
        {
            var response = new Response { ResponseText = "Failed to delete user!" };
            try
            {
                var result = await _dapper.GetAsync<Response>("Proc_DeleteUser", new
                {
                    userId,
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseText = "Something went wrong try after sometime!";
            }
            return response;
        }
        public async Task<Response<ApplicationUser>> FindByEmailAsync(string normalizedEmail)
        {
            var response = new Response<ApplicationUser> { ResponseText = "User not found!" };
            try
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
                var result = await _dapper.GetAsync<ApplicationUser>("Proc_FindUserByEmail", new
                {
                    normalizedEmail,
                }, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    response.StatusCode = ResponseStatus.Failed;
                    response.ResponseText = ResponseStatus.Failed.ToString();
                }
                response.Result = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseText = "Something went wrong try after sometime!";
            }
            return response;
        }
        public async Task<Response<ApplicationUser>> FindByMobileAsync(string mobileNo)
        {
            var response = new Response<ApplicationUser> { ResponseText = "User not found!" };
            try
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
                var result = await _dapper.GetAsync<ApplicationUser>("Proc_FindUserByMobile", new
                {
                    mobileNo,
                }, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    response.StatusCode = ResponseStatus.Failed;
                    response.ResponseText = ResponseStatus.Failed.ToString();
                }
                response.Result = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseText = "Something went wrong try after sometime!";
            }
            return response;
        }
        public async Task<Response<ApplicationUser>> FindByIdAsync(int userId)
        {
            var response = new Response<ApplicationUser> { ResponseText = "User not found!" };
            try
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
                var result = await _dapper.GetAsync<ApplicationUser>("Proc_FindUserById", new
                {
                    userId,
                }, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    response.StatusCode = ResponseStatus.Failed;
                    response.ResponseText = ResponseStatus.Failed.ToString();
                }
                response.Result = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseText = "Something went wrong try after sometime!";
            }
            return response;
        }
        public async Task<Response<ApplicationUser>> FindByNameAsync(string normalizedUserName)
        {
            var response = new Response<ApplicationUser> { ResponseText = "User not found!" };
            try
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
                var result = await _dapper.GetAsync<ApplicationUser>("Proc_FindUserByName", new
                {
                    normalizedUserName,
                }, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    response.StatusCode = ResponseStatus.Failed;
                    response.ResponseText = ResponseStatus.Failed.ToString();
                }
                response.Result = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseText = "Something went wrong try after sometime!";
            }
            return response;
        }
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            var result = false;
            try
            {
                result = await _dapper.GetAsync<bool>("Proc_IsInRoleAsync", new
                {
                    userId = user.Id,
                    roleName = roleName,
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }
        public async Task<Response<int>> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var response = new Response<int> { ResponseText = "User not found!" };
            return response;
        }
        public async Task<Response<IEnumerable<ApplicationRole>>> GetAllRolesAsync()
        {
            try
            {
                var result = await _dapper.GetAllAsync<ApplicationRole>("Proc_GetAllRoles", null, commandType: CommandType.StoredProcedure);
                return new Response<IEnumerable<ApplicationRole>>
                {
                    StatusCode = ResponseStatus.Success,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Response<IEnumerable<ApplicationRole>>
                {
                    StatusCode = ResponseStatus.Failed,
                    ResponseText = "Something went wrong try after sometime!"
                };
            }
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            var result = await _dapper.GetAllAsync<ApplicationUser>("Proc_GetAllUsers", null, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers(GetUserListReq getUserListReq)
        {
            var result = await _dapper.GetAllAsync<ApplicationUser>("Proc_GetAllUsers", getUserListReq, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task AddToRole(ApplicationUser user, string roleName)
        {
            var res = await _dapper.GetAsync<Response>("Proc_AddToRole", new
            {
                userId = user.Id,
                roleName
            }, commandType: CommandType.StoredProcedure);
        }
        public async Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            await _dapper.GetAsync<Response>("update [dbo].[Users] set AccessFailedCount = 0  where UserId = @UserId;select 1 StatusCode,'Success' ResponseText", new
            {
                user.Id,
            }, commandType: CommandType.Text);
        }
        public async Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd)
        {
            await _dapper.GetAsync<Response>("update [dbo].[Users] set lockoutEnd=@lockoutEnd where UserId=@UserId", new { user.Id, lockoutEnd }, commandType: CommandType.Text);
        }
        public async Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            string sp = "Update Users Set GAuthPin = @GAuthPin, TwoFactorEnabled = @TwoFactorEnabled where UserId = @UserId";
            user.TwoFactorEnabled = enabled;
            var res = _dapper.GetAsync<Response>(sp, new { user.TwoFactorEnabled, user.Id, user.GAuthPin }, commandType: CommandType.Text);
        }
        public async Task<List<string>> GetRolesAsync(int userId)
        {
            var response = new List<string>();
            try
            {
                var result = await _dapper.GetAsync<string>("Proc_GetRoles", new
                {
                    userId,
                }, commandType: CommandType.StoredProcedure);
                response.Add(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
