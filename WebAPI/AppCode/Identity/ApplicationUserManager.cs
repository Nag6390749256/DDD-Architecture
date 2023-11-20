using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace WebAPI.AppCode.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly ILogger<UserManager<ApplicationUser>> _logger;
        private readonly IUserService _userService;
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger, IUserService userService) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _logger = logger;
            _userService = userService;
        }
        public async Task<ApplicationUser> FindByMobileNoAsync(string mobileNo)
        {
            var response = new ApplicationUser();
            var userDeatils = await _userService.FindByMobileAsync(mobileNo);
            if(userDeatils.Result != null)
            {
                response = userDeatils.Result;
            }
            return response;
        }
        public override async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            var result = IdentityResult.Failed();
            return result;
        }
    }
}
