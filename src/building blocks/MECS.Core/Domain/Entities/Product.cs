using MECS.Core.Domain.Interfaces;
using MECS.Core.Domain.Validatiors;
using System;

namespace MECS.Core.Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public DateTime LastRegister { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ProductsValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
