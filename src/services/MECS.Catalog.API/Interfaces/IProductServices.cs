using MECS.Catalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.Catalog.API.Interfaces
{
    public interface IProductServices
    {
        Task<ProductViewModel> Get(Guid id);
        Task<IEnumerable<ProductViewModel>> Get();
    }
}
