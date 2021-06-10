using MECS.Catalog.API.Interfaces;
using MECS.Catalog.API.Models;
using MECS.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.Catalog.API.Controllers
{

    [Authorize]
    public class CatalogController : MainController
    {
        private readonly IProductServices _service;

        public CatalogController(IProductServices service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IEnumerable<ProductViewModel>> Get()
            => await _service.Get();

        //   [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("products/{id}")]
        public async Task<ProductViewModel> Get(Guid id)
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
