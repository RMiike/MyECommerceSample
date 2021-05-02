using MECS.Core.Domain.Entities;
using MECS.Identity.API.Extensions;
using MECS.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MECS.Identity.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class AuthController : Controller
    {

        private readonly SignInManager<IdentityUser> __signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager)
        {
            __signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserViewModel signUpUserViewModel)
        {
            var signUpUser = signUpUserViewModel.ConvertToEntity();
            if (!signUpUser.IsValid())
                return BadRequest();

            var user = new IdentityUser
            {
                UserName = signUpUser.Email,
                Email = signUpUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, signUpUser.Password);

            if (!result.Succeeded)
                return BadRequest();

            await __signInManager.SignInAsync(user, false);
            return Ok();

        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserViewModel signInUserViewModel)
        {
            var signInUser = signInUserViewModel.ConvertToEntity();
            if (!signInUser.IsValid())
                return BadRequest();

            var result = await __signInManager.PasswordSignInAsync(signInUser.Email, signInUser.Password, false, true);

            if (!result.Succeeded)
                return BadRequest();

            return Ok();
        }
        private async Task<SignInUserResponse> GenerateJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


        }
    }
}
