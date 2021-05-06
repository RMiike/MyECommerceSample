using MECS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MECS.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHaveErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
                return true;
            }
            return false;
        }
    }
}
