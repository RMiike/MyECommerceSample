using System;

namespace MECS.BFF.Purshases.Models
{
    public class ItemCartDTO
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
