using MECS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MECS.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        [Route("erro/sistema-indisponivel")]
        public IActionResult SistemaIndisponivel()
        {
            var modelErro = new ErrorViewModel()
            {
                Message = "O sistema está temporariamente indisponível. Isso pode ocorrer em momentos de sobrecarga de usuários",
                Title = "Sistema indisponivel",
                ErroCode = 500
            };
            return View("Error", modelErro);
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate o nosso suporte.";
                modelErro.Title = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Message = "A página que está procurando não existe! <br /> Em caso de dúvidas, entre em contato com o nosso suporte.";
                modelErro.Title = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Message = "Você não tem permissão para fazer isso.";
                modelErro.Title = "Acesso negado.";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }
            return View("Error", modelErro);
        }
    }
}
