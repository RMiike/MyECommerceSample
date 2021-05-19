using MECS.Core.Domain.Entities;
using MECS.Core.Helpers;
using MECS.Identity.API.Extensions;
using MECS.Identity.API.Models;
using MECS.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MECS.Identity.API.Controllers
{
    [Route("api/identity")]
    public class AuthController : MainController
    {

        private readonly SignInManager<IdentityUser> __signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings)
        {
            __signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserViewModel signUpUserViewModel)
        {
            var signUpUser = signUpUserViewModel.ConvertToEntity();
            if (!signUpUser.IsValid())
                return CustomResponse(signUpUser);

            var user = new IdentityUser
            {
                UserName = signUpUser.Email,
                Email = signUpUser.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, signUpUser.Password);

            if (result.Succeeded)
            {
                var token = await GenerateJWT(user.Email);
                return CustomResponse(token);
            }

            foreach (var erro in result.Errors)
            {
                AdicionarErroProcessamento(erro.Description);
            }
            return CustomResponse();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserViewModel signInUserViewModel)
        {
            var signInUser = signInUserViewModel.ConvertToEntity();
            if (!signInUser.IsValid())
                return CustomResponse(signInUser);

            var result = await __signInManager.PasswordSignInAsync(signInUser.Email, signInUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJWT(signInUser.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas.");
                return CustomResponse();
            }
            AdicionarErroProcessamento("Usuário ou senha incorretos.");
            return CustomResponse();
        }
        private async Task<SignInUserResponse> GenerateJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);

            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, user, claims);
        }
        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            return identityClaims;
        }
        private string CodificarToken(ClaimsIdentity claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }
        private SignInUserResponse ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {

            var response = new SignInUserResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(x => new UserClaim { Type = x.Type, Value = x.Value })
                }
            };
            return response;
        }

    }
}
