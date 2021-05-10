using MECS.Catalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.Catalog.API.Interfaces
{
    public interface IProductServices
    {
        Task<ProductsViewModel> Get(Guid id);
        Task<IEnumerable<ProductsViewModel>> Get();
    }
}
