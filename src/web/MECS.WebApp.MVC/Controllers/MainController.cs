using MECS.Core.Domain.Entities;
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
        protected void AdicionarErroValidacao(string mensagem)
        {
            ModelState.AddModelError(string.Empty, mensagem);
        }
        protected bool OperacaoValida()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
