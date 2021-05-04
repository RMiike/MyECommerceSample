using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Controllers
{
    public class IdentityController : Controller
    {

        private readonly IAuthService _authService;

        public IdentityController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp(SignUpUserViewModel signUpUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signUpUserViewModel);
            }

            var response = await _authService.SignUp(signUpUserViewModel);

            if (false)
            {
                return View(signUpUserViewModel);
            }


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn(SignInUserViewModel signInUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signInUserViewModel);
            }

            var response = await _authService.SignIn(signInUserViewModel);

            if (false)
            {
                return View(signInUserViewModel);
            }


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
