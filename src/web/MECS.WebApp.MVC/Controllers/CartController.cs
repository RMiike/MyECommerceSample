using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Controllers
{
    public class CartController : MainController
    {

        private readonly ICatalogService _catalogService;

        private readonly ICartService _cartService;

        public CartController(
            ICatalogService catalogService,
            ICartService cartService)
        {
            _catalogService = catalogService;
            _cartService = cartService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.Get());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddItemToCart(ItemProductViewModel model)
        {
            var product = await _catalogService.Get(model.IdProduct);

            ValidarCarrinho(product, model.Quantity);
            if (!OperacaoValida())
                return View("Index", await _cartService.Get());

            model.Name = product.Name;
            model.Price = product.Price;
            model.Image = product.Image;
            var response = await _cartService.AddItem(model);

            if (ResponseHaveErrors(response))
                return View("Index", await _cartService.Get());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateItemToCart(Guid idProduct, int quantity)
        {
            var product = await _catalogService.Get(idProduct);
            ValidarCarrinho(product, quantity);
            if (!OperacaoValida())
                return View("Index", await _cartService.Get());

            var item = new ItemProductViewModel { IdProduct = idProduct, Quantity = quantity };
            var response = await _cartService.UpdateItem(idProduct, item);

            if (ResponseHaveErrors(response))
                return View("Index", await _cartService.Get());

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> RemoveItemToCart(Guid idProduct)
        {
            var product = await _catalogService.Get(idProduct);
            if (product == null)
            {
                AdicionarErroValidacao("Produto inexistente!");
                return View("Index", await _cartService.Get());
            }
            var response = await _cartService.RemoveItem(idProduct);

            if (ResponseHaveErrors(response))
                return View("Index", await _cartService.Get());

            return RedirectToAction("Index");
        }
        private void ValidarCarrinho(ProductViewModel product, int quantity)
        {
            if (product == null)
                AdicionarErroValidacao("Produto inexistente!");

            if (quantity < 1)
                AdicionarErroValidacao($"Escolha ao menos uma unidade do produto {product.Name}");

            if (quantity > product.Stock)
                AdicionarErroValidacao($"O produto {product.Name} possui apenas {product.Stock} em estoque.");
        }
    }
}
