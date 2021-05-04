using MECS.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Interfaces
{
    public interface IAuthService
    {
        Task<string> SignIn(SignInUserViewModel signInUserViewModel);
        Task<string> SignUp(SignUpUserViewModel signUpUserViewModel);
    }
}
