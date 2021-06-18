using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Controllers
{
    public class CartController : MainController
    {


        private readonly IPurshaseBFFService _purshaseBFFService;

        public CartController(
            IPurshaseBFFService purshaseBFFService)
        {
            _purshaseBFFService = purshaseBFFService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _purshaseBFFService.Get());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddItemToCart(ItemProductViewModel model)
        {
            var response = await _purshaseBFFService.AddItem(model);
            if (ResponseHaveErrors(response))
                return View("Index", await _purshaseBFFService.Get());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateItemToCart(Guid idProduct, int quantity)
        {
            var item = new ItemProductViewModel { IdProduct = idProduct, Quantity = quantity };
            var response = await _purshaseBFFService.UpdateItem(idProduct, item);

            if (ResponseHaveErrors(response))
                return View("Index", await _purshaseBFFService.Get());

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> RemoveItemToCart(Guid idProduct)
        {
            var response = await _purshaseBFFService.RemoveItem(idProduct);

            if (ResponseHaveErrors(response))
                return View("Index", await _purshaseBFFService.Get());

            return RedirectToAction("Index");
        }
    }
}
