using Domain.Entities;
using Applications.Interface;
using System.Security.Claims;

namespace Applications.Services
{
    public class TokenService: ITokenService
    {
        private readonly Domain.Interface.ITokenService _tokenService;
        public TokenService(Domain.Interface.ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var token = _tokenService.GenerateAccessToken(claims);
            return token;
        }

        public RefreshTokenModel GenerateRefreshToken()
        {
            var token = _tokenService.GenerateRefreshToken();
            return token;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            return principal;
        }
    }
}
