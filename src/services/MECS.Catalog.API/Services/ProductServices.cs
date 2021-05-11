using MECS.Catalog.API.Extensions;
using MECS.Catalog.API.Interfaces;
using MECS.Catalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.Catalog.API.Services
{
    public class ProductServices : IProductServices
    {

        private readonly IProductRepository _repository;

        public ProductServices(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductViewModel> Get(Guid id)
        {
            var product = await _repository.Get(id);
            var viewModel = product.ConvertProductToViewModel();
            return viewModel;
        }

        public async Task<IEnumerable<ProductViewModel>> Get()
        {
            var products = await _repository.Get();
            var viewModels = products.ConvertProductToViewModel();
            return viewModels;
        }
    }
}
