using MECS.Order.Domain.Orders;
using System;

namespace MECS.Order.API.Application.DTO
{
    public class OrderItemDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public Guid IdProduct { get; set; }
        public decimal UnitValue { get; set; }
        public Guid IdOrder { get; set; }
        public static OrderItem ParaOrderItem(OrderItemDTO orderItemDTO)
        {
            return new OrderItem(
                orderItemDTO.IdProduct,
                orderItemDTO.Name,
                orderItemDTO.Quantity,
                orderItemDTO.UnitValue,
                orderItemDTO.Image);
        }
    }
}
