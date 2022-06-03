using Dapper;
using MECS.Order.API.Application.DTO;
using MECS.Order.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MECS.Order.API.Application.Queries
{
    public interface IOrderQueries
    {
        Task<OrderDTO> GetLastOrder(Guid idClient);
        Task<IEnumerable<OrderDTO>> GetOrdersByIdClient(Guid idClient);
    }

    public class OrderQueries : IOrderQueries
    {

        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO> GetLastOrder(Guid idClient)
        {

            const string sql = @"SELECT 
                                P.ID AS 'IdOrder', P.CODIGO, P.ISUSEDVOUCHER, P.DESCOUNT, P.TOTAL, P.ORDERSTATUS,
                                P.LOGRADOURO, P.NUMERO,P.BAIRRO,P.CEP,P.COMPLEMENTO,P.CIDADE,P.ESTADO,
                                PIT.ID AS 'IdProductItem', PIT.PRODUCTNAME, PIT.QUANTITY,PIT.PRODUCTIMAGE,PIT.UNITVALUE,
                                FROM Order P
                                INNER JOIN ORDERITEM
                                WHERE P.IDCLIENT = @idClient
                                AND P.INITIALDATA between DATEADD(minute,-3, GETDATE()) and DATEADD(minute, 0, GETDATE())
                                AND P.ORDERSTATUS = 1
                                ORDER BY P.INITIALDATA DESC";

            var order = await _orderRepository
                .GetConnection()
                .QueryAsync<dynamic>(sql, new { idClient });

            return MappOrder(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByIdClient(Guid idClient)
        {
            var orders = await _orderRepository.GetByIdClient(idClient);
            return orders.Select(OrderDTO.ToOrderDTO);
        }
        private OrderDTO MappOrder(dynamic result)
        {
            var order = new OrderDTO
            {
                Codigo = result[0].CODIGO,
                Status = result[0].ORDERSTATUS,
                Total = result[0].TOTAL,
                Descount = result[0].DESCOUNT,
                IsUsedVoucher = result[0].ISUSEDVOUCHER,

                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO
                {
                    Logradouro = result[0].LOGRADOURO,
                    Bairro = result[0].BAIRRO,
                    CEP = result[0].CEP,
                    Cidade = result[0].CIDADE,
                    Complemento = result[0].COMPLEMENTO,
                    Estado = result[0].ESTADO,
                    Numero = result[0].NUMERO,
                }
            };
            foreach (var item in result)
            {
                var orderItem = new OrderItemDTO
                {
                    Name = item.PRODUCTNAME,
                    UnitValue = item.UNITVALUE,
                    Quantity = item.QUANTITY,
                    Image = item.PRODUCTIMAGE,
                };
                order.OrderItems.Add(orderItem);
            }
            return order;
        }
    }
}
