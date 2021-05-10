using MECS.Catalog.API.Models;
using MECS.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MECS.Catalog.API.Extensions
{
    public static class MappingProduct
    {
        public static IEnumerable<ProductsViewModel> ConvertProductToViewModel(this IEnumerable<Product> products)
        {
            return new List<ProductsViewModel>(
                products.Select(product =>
                new ProductsViewModel
                {
                    Id = product.Id,
                    Description = product.Description,
                    Stock = product.Stock,
                    Image = product.Image,
                    IsActive = product.IsActive,
                    LastRegister = product.LastRegister,
                    Name = product.Name,
                    Price = product.Price
                }));
        }
        public static ProductsViewModel ConvertProductToViewModel(this Product product)
            => new ProductsViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Stock = product.Stock,
                Image = product.Image,
                IsActive = product.IsActive,
                LastRegister = product.LastRegister,
                Name = product.Name,
                Price = product.Price
            };
    }
}
