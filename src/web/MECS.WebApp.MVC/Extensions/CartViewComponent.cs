using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _service;

        public CartViewComponent(ICartService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _service.Get() ?? new CartViewModel());
        }
    }
}