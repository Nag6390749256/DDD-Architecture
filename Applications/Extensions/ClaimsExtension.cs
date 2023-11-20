using Domain.Enum;
using System.Security.Claims;

namespace Applications.Extensions
{
    public static class ClaimsExtension
    {
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            var loggedInUserId = principal.FindFirstValue("UserId");
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }
        public static T GetLoggedInUserBranchId<T>(this ClaimsPrincipal principal)
        {
            var loggedInUserId = principal.FindFirstValue("BranchId");
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }
        public static string GetLoggedInUserToken(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("Token");
        }
        public static ApplicationRoles GetLoggedInUserRole(this ClaimsPrincipal principal)
        {
            var enumValue = (ApplicationRoles)Enum.Parse(typeof(ApplicationRoles), principal.FindFirstValue(ClaimTypes.Role));
            return enumValue;
        }
    }
}
