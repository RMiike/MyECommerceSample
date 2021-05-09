using MECS.Core.Domain.Interfaces;
using MECS.Core.Domain.Validatiors;
using System;

namespace MECS.Core.Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public decimal Price { get; private set; }
        public DateTime LastRegister { get; private set; }
        public string Image { get; private set; }
        public int Stock { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new ProductsValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
