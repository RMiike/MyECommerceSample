using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Services
{
    public class AuthService : IAuthService
    {

        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SignIn(SignInUserViewModel signInUserViewModel)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(signInUserViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44374/api/identity/sign-in", loginContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> SignUp(SignUpUserViewModel signUpUserViewModel)
        {
            var registerContent = new StringContent(
               JsonSerializer.Serialize(signUpUserViewModel),
               Encoding.UTF8,
               "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44374/api/identity/sign-up", registerContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
