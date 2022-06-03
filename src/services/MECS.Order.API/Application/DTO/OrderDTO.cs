using MECS.Order.Domain.Orders;
using System;
using System.Collections.Generic;

namespace MECS.Order.API.Application.DTO
{
    public class OrderDTO
    {

        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public int Status { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; set; }
        public decimal Descount { get; set; }
        public string VoucherCodigo { get; set; }
        public bool IsUsedVoucher { get; set; }
        public AddressDTO Address { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public static OrderDTO ToOrderDTO(Orders order)
        {
            var orderDTO = new OrderDTO
            {
                Id = order.Id,
                Codigo = order.Codigo,
                Status = (int) order.OrderStatus,
                Data = order.InitialDate,
                Total = order.Total,
                Descount = order.Descount,
                IsUsedVoucher = order.IsUsedVoucher,
                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO(),
            };

            foreach (var item in order.OrderItems)
            {
                orderDTO.OrderItems.Add(new OrderItemDTO
                {
                    Name = item.ProductName,
                    Image = item.ProductImage,
                    Quantity = item.Quantity,
                    IdProduct = item.IdProduct,
                    UnitValue = item.UnitValue,
                    IdOrder = item.IdOrder
                });
            };

            orderDTO.Address = new AddressDTO
            {
                Logradouro = order.Address.Logradouro,
                Numero = order.Address.Numero,
                Complemento = order.Address.Complemento,
                Bairro = order.Address.Bairro,
                CEP = order.Address.CEP,
                Cidade = order.Address.Cidade,
                Estado = order.Address.Estado
            };

            return orderDTO;
        }
    }
}
