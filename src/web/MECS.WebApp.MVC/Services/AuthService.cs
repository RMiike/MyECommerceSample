using MECS.Core.Domain.Entities;
using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient,
                  IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.AuthUrl);
            _httpClient = httpClient;
        }
        public async Task<SignInUserResponse> SignIn(SignInUserViewModel signInUserViewModel)
        {
            var loginContent = GetContent(signInUserViewModel);
            var response = await _httpClient.PostAsync($"/api/identity/sign-in", loginContent);
            if (!TreateErrorsResponse(response))
            {
                return new SignInUserResponse
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }
            return await DeserializeObjectResponse<SignInUserResponse>(response);
        }
        public async Task<SignInUserResponse> SignUp(SignUpUserViewModel signUpUserViewModel)
        {
            var registerContent = GetContent(signUpUserViewModel);
            var response = await _httpClient.PostAsync($"/api/identity/sign-up", registerContent);
            if (!TreateErrorsResponse(response))
            {
                return new SignInUserResponse
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }
            return await DeserializeObjectResponse<SignInUserResponse>(response);
        }

    }
}
