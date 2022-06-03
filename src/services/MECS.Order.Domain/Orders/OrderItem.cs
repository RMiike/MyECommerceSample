using MECS.Core.Domain.Entities;
using System;

namespace MECS.Order.Domain.Orders
{
    public class OrderItem : BaseEntity
    {
        protected OrderItem() { }
        public OrderItem(
                         Guid idProduct,
                         string productName,
                         int quantity,
                         decimal unitValue,
                         string productImage = null)
        {
            IdProduct = idProduct;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
            ProductImage = productImage;
        }

        public Guid IdOrder { get; private set; }
        public Guid IdProduct { get; private set; }
        public string ProductName { get; private set; }
        public string ProductImage { get; set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public Orders Order { get; set; }
        internal decimal CalcularValor()
        {
            return Quantity * UnitValue;
        }
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
