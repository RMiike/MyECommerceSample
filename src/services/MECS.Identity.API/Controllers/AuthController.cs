using MECS.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MECS.Identity.API.Controllers
{
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
        public async Task<IActionResult> SignUp(SignUpUser signUpUser)
        {
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
        public async Task<IActionResult> SignIn(SignInUser signInUser)
        {
            if (!signInUser.IsValid())
                return BadRequest();

            var result = await __signInManager.PasswordSignInAsync(signInUser.Email, signInUser.Password, false, true);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }
    }
}
