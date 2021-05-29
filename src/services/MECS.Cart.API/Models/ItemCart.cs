using System;

namespace MECS.Cart.API.Models
{
    public class ItemCart
    {
        public ItemCart()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid IdCart { get; set; }
        public ClientCart ClientCart { get; set; }
    }
}
