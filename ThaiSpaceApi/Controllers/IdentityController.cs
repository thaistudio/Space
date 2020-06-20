using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SpaceServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ThaiSpaceApi.Models;
using ThaiSpaceApi.Services;

namespace ThaiSpaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<SpaceUser> userManager;
        private readonly SignInManager<SpaceUser> signInManager;
        private readonly IEmailSender emailSender;

        public IdentityController(UserManager<SpaceUser> userManager,
                                SignInManager<SpaceUser> signInManager,
                                IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SpaceUser spaceUser)
        {
            if (spaceUser is null)
                return BadRequest(new ApiResponse<SpaceUser>
                {
                    Message = "Register failed",
                    Data = spaceUser
                });

            var user = spaceUser;
            user.UserName = spaceUser.Email;

            var result = await userManager.CreateAsync(user, spaceUser.PasswordHash);

            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim("Jobs", "Dev"));

                // Send activation email
                var activationKey = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedKey = HttpUtility.UrlEncode(activationKey);
                //var user1 = await userManager.FindByEmailAsync(user.Email);
                //var confirm = await userManager.ConfirmEmailAsync(user1, activationKey);
                var message = $"https://localhost:44314/api/identity/active?key={encodedKey}&email={user.Email}";
                await emailSender.SendEmailAsync("moutainqueen@gmail.com", "Hi", message);

                return Ok(new ApiResponse<SpaceUser>
                {
                    Message = "Registered successfully!",
                    Data = user
                });
            }

            return BadRequest(new ApiResponse<SpaceUser>
            {
                Message = "Register failed",
                Data = spaceUser
            });
        }

        [HttpGet("active")]
        public async Task<IActionResult> Activate(string key, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var confirm = await userManager.ConfirmEmailAsync(user, key);

            if (confirm.Succeeded)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("test")]
        public async Task<string> Test()
        {
            try
            {
                await emailSender.SendEmailAsync("moutainqueen@gmail.com", "Hi", "Email sent");
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return "Api works";
        }
    }
}
