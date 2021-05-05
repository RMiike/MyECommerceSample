using MECS.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Interfaces
{
    public interface IAuthService
    {
        Task<SignInUserResponse> SignIn(SignInUserViewModel signInUserViewModel);
        Task<SignInUserResponse> SignUp(SignUpUserViewModel signUpUserViewModel);
    }
}
