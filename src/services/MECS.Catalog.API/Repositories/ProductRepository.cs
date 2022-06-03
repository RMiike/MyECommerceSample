using MECS.Catalog.API.Data;
using MECS.Catalog.API.Interfaces;
using MECS.Core.Data.Interface;
using MECS.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MECS.Catalog.API.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly CatalogContext _context;
    public ProductRepository(CatalogContext context)
    {
      _context = context;
    }
    public IUnitOfWork UnitOfWork => _context;
    public async Task<IEnumerable<Product>> Get()
    {
      return await _context.Products.ToListAsync();
    }
    public async Task<Product> Get(Guid id)
    {
      return await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
    }
    public void Post(Product product)
    {
      _context.Products.Add(product);
    }
    public void Put(Product product)
    {
      _context.Products.Update(product);
    }

  }
}
