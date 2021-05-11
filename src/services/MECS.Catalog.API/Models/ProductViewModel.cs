using System;

namespace MECS.Catalog.API.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public DateTime LastRegister { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
    }
}
