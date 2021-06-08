using FluentValidation;
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
        internal void AssociarCarrinho(Guid idCart)
        {
            IdCart = idCart;
        }
        internal decimal CalcularValor()
        {
            return Quantity * Price;
        }
        internal void AddUnits(int unit)
        {
            Quantity += unit;
        }
        internal void UpdateUnit(int unit)
        {
            Quantity = unit;
        }

        internal bool IsValid()
        {
            return new ItemValidation().Validate(this).IsValid;
        }

        public class ItemValidation : AbstractValidator<ItemCart>
        {
            public ItemValidation()
            {
                RuleFor(c => c.IdProduct)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado");

                RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                    .WithMessage("A quantidade mínima é 1.");

                RuleFor(c => c.Quantity)
                    .LessThan(ClientCart.MAX_QUANTITY_ITEM)
                    .WithMessage($"A quantidade máxima é {ClientCart.MAX_QUANTITY_ITEM}");

                RuleFor(c => c.Price)
                    .GreaterThan(0)
                    .WithMessage("O preço do Item precisa ser maior que 0.");
            }
        }
    }
}
