using MECS.Core.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace MECS.Order.Domain.Orders
{
    public interface IOrderRepository : IRepository<Orders>
    {
        Task<Orders> GetOrderById(Guid id);
        Task<IEnumerable<Orders>> GetByIdClient(Guid idClient);
        void Add(Orders order);
        void Update(Orders order);

        DbConnection GetConnection();
        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid idOrder, Guid idProduct);

    }
}
