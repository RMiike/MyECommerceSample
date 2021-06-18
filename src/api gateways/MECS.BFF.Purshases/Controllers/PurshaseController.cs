using MECS.BFF.Purshases.Models;
using MECS.BFF.Purshases.Services;
using MECS.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace MECS.BFF.Purshases.Controllers
{
    [Authorize]
    public class PurshaseController : MainController
    {

        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public PurshaseController(
            ICartService cartService,
            ICatalogService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _cartService.Get());
        }

        [HttpGet]
        [Route("cart-quantity")]
        public async Task<int> GetQuantity()
        {
            var quantity = await _cartService.Get();
            return quantity?.Itens.Sum(i => i.Quantity) ?? 0;
        }

        [HttpPost]
        [Route("cart/itens")]
        public async Task<IActionResult> AddItemToCart(ItemCartDTO itemCart)
        {
            var product = await _catalogService.Get(itemCart.IdProduct);
            await ValidateItemCart(product, itemCart.Quantity);
            if (!OperacaoValida())
                return CustomResponse();

            itemCart.Name = product.Name;
            itemCart.Price = product.Price;
            itemCart.Image = product.Image;

            var response = await _cartService.AddItem(itemCart);

            return CustomResponse(response);
        }
        [HttpPut]
        [Route("cart/itens/{idProduct}")]
        public async Task<IActionResult> UpdateItemToCart(Guid idProduct, ItemCartDTO itemCart)
        {
            var product = await _catalogService.Get(idProduct);
            await ValidateItemCart(product, itemCart.Quantity);
            if (!OperacaoValida())
                return CustomResponse();

            var response = await _cartService.UpdateItem(idProduct, itemCart);

            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("cart/itens/{idProduct}")]
        public async Task<IActionResult> RemoveItemToCart(Guid idProduct)
        {
            var product = await _catalogService.Get(idProduct);
            if (product == null)
            {
                AdicionarErroProcessamento("Produto inexistente.");
                return CustomResponse();
            }
            var response = await _cartService.RemoveItem(idProduct);

            return CustomResponse(response);
        }
        private async Task ValidateItemCart(ItemProductDTO product, int quantity)
        {
            if (product == null)
                AdicionarErroProcessamento("Produto inexistente.");
            if (quantity < 1)
                AdicionarErroProcessamento($"Escolha ao menos um {product.Name}");

            var cart = await _cartService.Get();
            var itensCart = cart.Itens.FirstOrDefault(p => p.IdProduct == product.Id);

            if (itensCart != null && itensCart.Quantity + quantity > product.Stock)
            {
                AdicionarErroProcessamento($"O produto {product.Name} possui {product.Stock} unidades em estoque. Você selecionou {quantity}");
                return;
            }

            if (quantity > product.Stock)
                AdicionarErroProcessamento($"O produto {product.Name} possui {product.Stock} unidades. Você selecionou {quantity}");
        }
    }
}
