using Applications.Models;
using Domain.Entities;
using Applications.Interface;
using Infrastructure;
using Infrastructure.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.AppCode.Identity;
using System.Reflection;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(ApplicationUserManager userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        [HttpPost(nameof(SignUp))]
        public async Task<IActionResult> SignUp(SignUpReq signUpReq)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Failed to signup."
            };
            var isRoleExists = Enum.GetValues(typeof(Domain.Enum.ApplicationRoles))
                                               .Cast<Domain.Enum.ApplicationRoles>()
                                               .ToList().Any(x=>x == signUpReq.RoleId) && signUpReq.RoleId != Domain.Enum.ApplicationRoles.Admin;
            if (!isRoleExists)
            {
                response.ResponseText = "Invalid role found.";
                goto Finish;
            }
            var user = new ApplicationUser
            {
                UserName = signUpReq.MobileNo.Trim(),
                Email = signUpReq.Email.Trim(),
                RoleId = signUpReq.RoleId,
                FirstName = signUpReq.FirstName,
                LastName = signUpReq.LastName,
                MeddleName = signUpReq.MiddleName,
                WhatsAppNumber = signUpReq.WhatsappNo,
                AlternateNumber = signUpReq.AlternateNo,
                PhoneNumber = signUpReq.MobileNo,
                CityId = signUpReq.CityId,
                Address = signUpReq.Address,
                PostalCode = signUpReq.PostalCode,
                Gender = signUpReq.Gender,
            };
            var result = await _userManager.CreateAsync(user, signUpReq.Password);
            if (!result.Succeeded)
            {
                response.ResponseText = "An error has ocurred in signup. please try again later";
                goto Finish;
            }
            if (result.Succeeded)
            {
                response.ResponseText = "Signup successfully.";
                response.StatusCode = ResponseStatus.Success;
                goto Finish;
            }
            Finish:
            return Ok(response);
        }
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var response = new Response<LoginResponse>();
            var user = await _userManager.FindByEmailAsync(loginRequest.EmailOrMobile)??
                       await _userManager.FindByMobileNoAsync(loginRequest.EmailOrMobile);
            if (user == null)
            {
                response.ResponseText = "User not found!";
                return BadRequest(response);
            }
            var IsPassvalid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!IsPassvalid)
            {
                response.ResponseText = "Invalid username or password!";
                return Unauthorized(response);
            }
            if (!user.IsActive)
            {
                response.ResponseText = "User inactive please actived yourself before login!";
                return BadRequest(response);
            }
            var result = await _signInManager.PasswordSignInAsync(user.Email,loginRequest.Password, loginRequest.RememberMe, false);
            if (!result.Succeeded)
            {
                response.ResponseText = "Invalid username or password!";
                return Unauthorized(response);
            }
            var roleDetails = await _userManager.GetRolesAsync(user);
            var cliamList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, roleDetails.FirstOrDefault()??""),
            };
            var refreshToken = _tokenService.GenerateRefreshToken();
            string token = _tokenService.GenerateAccessToken(cliamList);
            response.StatusCode = ResponseStatus.Success;
            response.ResponseText = ResponseStatus.Success.ToString();
            response.Result = new LoginResponse
            {
                Email = user.Email,
                UserId = user.Id,
                UserName = user.UserName, 
                Name = user.UserName,
                PhoneNumber  = user.PhoneNumber,
                RefreshToken = refreshToken.RefreshToken,
                Token = token,
                Role = roleDetails.FirstOrDefault(),
            };
            return Ok(response);
        } 
    }
}
