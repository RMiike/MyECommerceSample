using MECS.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Interfaces
{
    public interface ICatalogService
    {
        Task<ProductViewModel> Get(Guid id);
        Task<IEnumerable<ProductViewModel>> Get();
    }
}
