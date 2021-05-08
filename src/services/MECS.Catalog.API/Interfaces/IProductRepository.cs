using MECS.Core.Data.Interface;
using MECS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MECS.Catalog.API.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> Get();
        Task<Product> Get(Guid id);
        void Post(Product product);
        void Put(Product product);
    }
}
