using Domain.Entities;
using System.Security.Claims;

namespace Applications.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        RefreshTokenModel GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
