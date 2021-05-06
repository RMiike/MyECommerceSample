using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {

        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SignInUserResponse> SignIn(SignInUserViewModel signInUserViewModel)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(signInUserViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44374/api/identity/sign-in", loginContent);

            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!TreateErrorsResponse(response))
            {
                return new SignInUserResponse
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), opt)
                };
            }


            return JsonSerializer.Deserialize<SignInUserResponse>(await response.Content.ReadAsStringAsync(), opt);
        }

        public async Task<SignInUserResponse> SignUp(SignUpUserViewModel signUpUserViewModel)
        {
            var registerContent = new StringContent(
               JsonSerializer.Serialize(signUpUserViewModel),
               Encoding.UTF8,
               "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44374/api/identity/sign-up", registerContent);
            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!TreateErrorsResponse(response))
            {
                return new SignInUserResponse
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), opt)
                };
            }


            return JsonSerializer.Deserialize<SignInUserResponse>(await response.Content.ReadAsStringAsync(), opt);
        }

    }
}
