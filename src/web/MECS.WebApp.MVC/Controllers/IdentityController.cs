﻿using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            //if (false)
            //{
            //    return View(signUpUserViewModel);
            //}
            await SignIn(response);


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

            //if (false)
            //{
            //    return View(signInUserViewModel);
            //}

            await SignIn(response);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }
        private async Task SignIn(SignInUserResponse response)
        {
            var token = GetFormatedToken(response.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
        private JwtSecurityToken GetFormatedToken(string token)
            => new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
    }
}
