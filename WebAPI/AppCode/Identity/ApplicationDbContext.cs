using Dapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.AppCode.Identity
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IQueryable<ApplicationRole> Roles()
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return _dbConnection.Query<ApplicationRole>("select * from ApplicationRole(nolock)", commandType: CommandType.Text).AsQueryable();
            }
        }

        public IQueryable<ApplicationUser> Users()
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return _dbConnection.Query<ApplicationUser>("select * from Users(nolock) where ID<>1", commandType: CommandType.Text).AsQueryable();
            }
        }

        public Task<IdentityResult> CreateRole(ApplicationRole role)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                if (_dbConnection.Execute("AddRole", role, commandType: CommandType.StoredProcedure) > 0)
                {
                    return Task.FromResult(IdentityResult.Success);
                }
                else
                {
                    return Task.FromResult(IdentityResult.Failed());
                }
            }
        }

        public void AddToRoleAsync(int id, int roleid)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                _dbConnection.Execute("insert into RoleId values('" + id + "','" + roleid + "')", null, commandType: CommandType.Text);
            }
        }

        public Task<ApplicationRole> FindRoleByIdAsync(string roleId)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                var role = _dbConnection.Query<ApplicationRole>("select * from ApplicationRole(nolock) where UserId='" + roleId + "'", null, commandType: CommandType.Text).FirstOrDefault();
                return Task.FromResult(role);
            }
        }

        public Task<ApplicationRole> FindRoleByNameAsync(string normalizedRoleName)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                var role = _dbConnection.Query<ApplicationRole>("select * from ApplicationRole(nolock) where NormalizedName='" + normalizedRoleName + "'", null, commandType: CommandType.Text).FirstOrDefault();
                return Task.FromResult(role);
            }
        }

        public IdentityResult AddUser(ApplicationUser user)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                var res = _dbConnection.Execute("AddUser", user, commandType: CommandType.StoredProcedure);
                if (res > 0)
                {
                    return IdentityResult.Success;
                }
                else if (res < 0)
                {
                    var v = new IdentityError()
                    {
                        Code = "-1",
                        Description = res == -1 ? "Mobile Or EmailID already exists." : res == -2 ? "Email already exists!" : "Something went wrong! Try again later."
                    };
                    var resp = IdentityResult.Failed(v);
                    return resp;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            }
        }

        public string GetUserName(ApplicationUser user)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return _dbConnection.Query<string>("select EmailOrMobile from Users(nolock) where EmailOrMobile='" + user.UserName + "'", null, commandType: CommandType.Text).FirstOrDefault();
            }
        }

        public Task<ApplicationUser> FindByEmailAsync(string normalizedEmail)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return Task.FromResult(_dbConnection.Query<ApplicationUser>("select * from Users(nolock) where NormalizedEmail='" + normalizedEmail + "'", null, commandType: CommandType.Text).FirstOrDefault());
            }

        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return Task.FromResult(_dbConnection.Query<ApplicationUser>("select * from Users(nolock) where UserId='" + userId + "'", null, commandType: CommandType.Text).FirstOrDefault());
            }

        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return Task.FromResult(_dbConnection.Query<ApplicationUser>("select * from Users(nolock) where NormalizedUserName='" + normalizedUserName + "'", null, commandType: CommandType.Text).FirstOrDefault());
            }

        }


        public Task<List<string>> GetRolesAsync(ApplicationUser user)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return Task.FromResult(_dbConnection.Query<string>("proc_getUserRole", new { UserId = user.Id, Email = user.Email }, commandType: CommandType.StoredProcedure).ToList());
                //return Task.FromResult(_dbConnection.Query<string>("GetUserRoles", new { UserId = user.Id }, commandType: CommandType.StoredProcedure).ToList());
            }


        }

        public string GetNormalizedUserName(ApplicationUser user)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return _dbConnection.Query<string>("select NormalizedUserName from Users(nolock) where Email='" + user.Email + "'", null, commandType: CommandType.Text).FirstOrDefault();
            }

        }

        public Task<string> GetEmailAsync(string email)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {


                return Task.FromResult(_dbConnection.Query<string>("select Email from Users(nolock) where EmailOrMobile='" + email + "'", null, commandType: CommandType.Text).FirstOrDefault());
            }
        }

        public string GetUserIdAsync(string userName)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {


                return _dbConnection.Query<string>("select UserId from Users(nolock) where EmailOrMobile='" + userName + "'", null, commandType: CommandType.Text).FirstOrDefault();
            }
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, int roleId)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return Task.FromResult(_dbConnection.Query("select UserId from RoleId(nolock) where RoleId='" + roleId + "' and UserId='" + user.Id + "'", null, commandType: CommandType.Text).Any());
            }
        }

        public Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            using (var _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                if (_dbConnection.Execute("UpdateUser", user, commandType: CommandType.StoredProcedure) > 0)
                {
                    return Task.FromResult(IdentityResult.Success);
                }
                else
                {

                    return Task.FromResult(IdentityResult.Failed());
                }
            }
        }
    }
}
