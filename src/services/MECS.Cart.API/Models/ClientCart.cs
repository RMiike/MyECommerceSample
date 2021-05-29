using System;
using System.Collections.Generic;

namespace MECS.Cart.API.Models
{
    public class ClientCart
    {
        protected ClientCart() { }
        public ClientCart(Guid idClient)
        {
            Id = Guid.NewGuid();
            IdClient = idClient;
        }
        public Guid Id { get; set; }
        public Guid IdClient { get; set; }
        public decimal Total { get; set; }
        public List<ItemCart> Itens { get; set; } = new List<ItemCart>();
    }
}
