using MECS.Catalog.API.Interfaces;
using MECS.Catalog.API.Models;
using MECS.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.Catalog.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly IProductServices _service;

        public CatalogController(IProductServices service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("catalog/products")]
        public async Task<IEnumerable<ProductsViewModel>> Get()
            => await _service.Get();

        [ClaimsAuthorize("Catalog","Ler")]
        [HttpGet("catalog/products/{id}")]
        public async Task<ProductsViewModel> Get(Guid id)
            => await _service.Get(id);

        //[HttpGet("catalog/products/{id}")]

        //public void Post([FromBody] string value)
        //{
        //}

        //[HttpGet("catalog/products/{id}")]

        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpGet("catalog/products/{id}")]

        //public void Delete(int id)
        //{
        //}
    }
}
