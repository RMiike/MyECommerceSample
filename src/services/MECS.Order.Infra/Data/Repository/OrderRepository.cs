using MECS.Core.Data.Interface;
using MECS.Order.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MECS.Order.Infra.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
        public DbConnection GetConnection() => _context.Database.GetDbConnection();
        public async Task<Orders> GetOrderById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }
        public async Task<IEnumerable<Orders>> GetByIdClient(Guid idClient)
        {
            return await _context.Orders
                .Include(p => p.OrderItems)
                .AsNoTracking()
                .Where(p => p.IdClient == idClient)
                .ToListAsync();
        }
        public void Add(Orders order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Orders order)
        {
            _context.Orders.Update(order);

        }
        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);

        }

        public async Task<OrderItem> GetItemByOrder(Guid idOrder, Guid idProduct)
        {
            return await _context.OrderItems
                .FirstOrDefaultAsync(p => p.IdOrder == idOrder && p.IdProduct == idProduct);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
