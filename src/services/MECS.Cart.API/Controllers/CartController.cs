using MECS.Cart.API.Models;
using MECS.WebAPI.Core.Controllers;
using MECS.WebAPI.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MECS.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {

        private readonly IAspNetUser _user;

        [HttpGet]
        public async Task<ClientCart> Get()
        {
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Post(ItemCart item)
        {
            return CustomResponse();
        }
        [HttpPut]
        public async Task<IActionResult> Put(Guid idProduct, ItemCart item)
        {
            return CustomResponse();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid idProduct)
        {
            return CustomResponse();
        }
    }
}
