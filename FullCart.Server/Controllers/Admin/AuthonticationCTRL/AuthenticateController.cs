using Application.Exceptions;
using Infrastructure.UOW;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Enums;
using Domain.Entities.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using FullCart.Server.Controllers.Admin.Responses;

namespace FullCart.Server.Controllers.Admin.AuthonticationCTRL
{
    public class AuthenticateController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UOW UOW { get; }

        private IConfiguration configuration;

        public IMediator Mediatr { get; }

        public AuthenticateController(UserManager<ApplicationUser> userManager, IConfiguration configuration, UOW _uow, IMediator _Mediatr, ILogger<BaseController> Logger) : base(Logger)
        {
            this.userManager = userManager;
            UOW = _uow;
            this.configuration = configuration;
            Mediatr = _Mediatr;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                throw new FullCartException(ValidationKey.InvalidCredentialsTryAgain);
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null || user.IsDeleted || user.RoleID != (int)RoleType.Admin)
            {
                throw new FullCartException(ValidationKey.InvalidCredentialsTryAgain);
            }

            if (user.LockoutEnd > DateTime.UtcNow)
            {
                if (user.AccessFailedCount >= configuration.GetValue<int>("Auth:MaxFailedAccessAttemptsBeforeLockout"))
                    throw new FullCartException(ValidationKey.YouHaveExcedRetrailCount);
                throw new FullCartException(ValidationKey.AccountIslLocked);
            }
            else
            {

                if (!await userManager.CheckPasswordAsync(user, model.Password))
                {
                    //var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    //var resetPassResult = await userManager.ResetPasswordAsync(user, token, model.Password);
                    await userManager.AccessFailedAsync(user);
                    throw new FullCartException(ValidationKey.InvalidCredentialsTryAgain);
                }
                else
                {
                    if (user.AccessFailedCount > 0)
                        await userManager.ResetAccessFailedCountAsync(user);

                    var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                        {
                        new(ClaimTypes.Name, user.UserName),
                        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var credentials = new SigningCredentials(
                                      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                                      SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        expires: DateTime.Now.AddDays(configuration.GetValue<int>("JWT:ExpireInDays")),
                        claims: authClaims,
                        signingCredentials: credentials
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        DefaultLanguageId = user.FK_DefaultLanguageID
                    });
                }

            }
            return Unauthorized();

        }

        [Authorize]
        [HttpPost]
        [Route("logout")]
        public IActionResult logout()
        {
            var user = UOW.AccountRepo.FindAccount(User.Identity.Name);
            if (user != null)
            {
                user.OnEvent += OnAggregateEvent;
                user.Logout(user.Id);
                UOW.SaveChanges();
            }

            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("GetUserData")]
        public ActionResult GetUserData()
        {
            var myUser = UOW.AccountRepo.FindAccount(User.Identity.Name);
            if (myUser != null)
            {
                var customer = UOW.CustomerRepo.GetCustomerData(myUser.Id);
                var ProfilePicturePath = $"{Request.Scheme}://{Request.Host}/Attachments/Customers/{customer.Id}/{customer.ProfilePicture}";
                var userData = new UserDataResponse
                {
                    DisplayName = customer.FirstName + " " + customer.FamilyName,
                    CustomerId = customer.Id,
                    ProfilePicture = ProfilePicturePath,
                    MobileNumber = customer.Mobile
                };

                return Ok(userData);
            }
            return null;

        }

        private void OnAggregateEvent(object sender, EventArgs e)
        {
            if (Mediatr.Publish(e) is var mediatrTask && mediatrTask.IsFaulted) throw mediatrTask.Exception;
        }
    }
}
