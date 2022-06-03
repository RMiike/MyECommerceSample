using MECS.Cart.API.Data;
using MECS.Cart.API.Models;
using MECS.WebAPI.Core.Controllers;
using MECS.WebAPI.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MECS.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _context;
        public CartController(IAspNetUser user, CartContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet]
        public async Task<ClientCart> Get()
        {
            return await ObterCarrinhoCliente() ?? new ClientCart();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ItemCart item)
        {
            var cart = await ObterCarrinhoCliente();
            if (cart == null)
                ManipularNovoCarrinho(item);
            else
                ManipularCarrinhoExistente(cart, item);

            if (!OperacaoValida())
                return CustomResponse();

            await PersistirDados();

            return CustomResponse();
        }

        [HttpPut("{idProduct}")]
        public async Task<IActionResult> Put(Guid idProduct, ItemCart item)
        {
            var cart = await ObterCarrinhoCliente();
            var itemCart = await GetValidItemCart(idProduct, cart, item);


            if (itemCart == null)
                return CustomResponse();

            cart.UpdateUnit(itemCart, item.Quantity);
            ValidarCarrinho(cart);
            if (!OperacaoValida())
                return CustomResponse();

            _context.ItensCart.Update(itemCart);
            _context.ClientCart.Update(cart);

            await PersistirDados();

            return CustomResponse();
        }

        [HttpDelete("{idProduct}")]
        public async Task<IActionResult> Delete(Guid idProduct)
        {
            var cart = await ObterCarrinhoCliente();
            var itemCart = await GetValidItemCart(idProduct, cart);
            if (itemCart == null)
            {
                return CustomResponse();
            }
            ValidarCarrinho(cart);
            if (!OperacaoValida())
                return CustomResponse();

            cart.RemoveItem(itemCart);

            _context.ItensCart.Remove(itemCart);
            _context.ClientCart.Update(cart);

            await PersistirDados();

            return CustomResponse();
        }

        [HttpPost("add-voucher")]
        public async Task<IActionResult> AddVoucher(Voucher voucher)
        {
            var cart = await ObterCarrinhoCliente();

            cart.AddVoucher(voucher);

            _context.ClientCart.Update(cart);

            var result = await _context.SaveChangesAsync();
            if (result <= 0)
                AdicionarErroProcessamento("Não foi possível persistir os dados no banco");

            return CustomResponse();
        }
        private async Task<ClientCart> ObterCarrinhoCliente()
        {
            var cart = await _context.ClientCart
                            .Include(c => c.Itens)
                            .FirstOrDefaultAsync(c => c.IdClient == _user.GetUserId());
            return cart;
        }
        private void ManipularNovoCarrinho(ItemCart item)
        {
            var cart = new ClientCart(_user.GetUserId());
            cart.AddItem(item);
            ValidarCarrinho(cart);
            _context.ClientCart.Add(cart);
        }
        private void ManipularCarrinhoExistente(ClientCart cart, ItemCart item)
        {
            var produtoItemExistente = cart.ItemExists(item);
            cart.AddItem(item);
            ValidarCarrinho(cart);
            if (produtoItemExistente)
            {
                _context.ItensCart.Update(cart.GetItemByIdProduct(item.IdProduct));
            }
            else
            {
                _context.ItensCart.Add(item);
            }
            _context.ClientCart.Update(cart);
        }
        private async Task<ItemCart> GetValidItemCart(Guid idProduct, ClientCart cart, ItemCart item = null)
        {
            if (item != null && idProduct != item.IdProduct)
            {
                AdicionarErroProcessamento("O item não corresponde ao informado.");
                return null;
            }

            if (cart == null)
            {
                AdicionarErroProcessamento("Carrinho não encontrado");
                return null;
            }

            var itemCart = await _context.ItensCart
                .FirstOrDefaultAsync(i => i.IdCart == cart.Id && i.IdProduct == idProduct);

            if (itemCart == null || !cart.ItemExists(itemCart))
            {
                AdicionarErroProcessamento("O item não está no carrinho.");
                return null;
            }

            return itemCart;
        }
        private async Task PersistirDados()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
                AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }
        private bool ValidarCarrinho(ClientCart cart)
        {
            if (cart.IsValid())
                return true;
            cart.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }
    }
}
