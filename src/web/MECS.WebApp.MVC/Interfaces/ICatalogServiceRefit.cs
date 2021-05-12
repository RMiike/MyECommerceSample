using MECS.WebApp.MVC.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Interfaces
{
    public interface ICatalogServiceRefit
    {
        [Get("/catalog/products/")]
        Task<IEnumerable<ProductViewModel>> Get();

        [Get("/catalog/products/{id}")]
        Task<ProductViewModel> Get(Guid id);
    }
}
