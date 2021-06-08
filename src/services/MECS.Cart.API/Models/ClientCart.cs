using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MECS.Cart.API.Models
{
    public class ClientCart
    {
        internal const int MAX_QUANTITY_ITEM = 5;
        public ClientCart() { }
        public ClientCart(Guid idClient)
        {
            Id = Guid.NewGuid();
            IdClient = idClient;
        }
        public Guid Id { get; set; }
        public Guid IdClient { get; set; }
        public decimal Total { get; set; }
        public List<ItemCart> Itens { get; set; } = new List<ItemCart>();
        public ValidationResult ValidationResult { get; set; }
        internal void CalcularValorCarrinho()
        {
            Total = Itens.Sum(p => p.CalcularValor());
        }
        internal bool ItemExists(ItemCart item)
        {
            return Itens.Any(p => p.IdProduct == item.IdProduct);
        }
        internal ItemCart GetItemByIdProduct(Guid idProduct)
        {
            return Itens.FirstOrDefault(p => p.IdProduct == idProduct);
        }
        internal void AddItem(ItemCart item)
        {


            item.AssociarCarrinho(Id);
            if (ItemExists(item))
            {
                var itemCart = GetItemByIdProduct(item.IdProduct);
                itemCart.AddUnits(item.Quantity);

                item = itemCart;
                Itens.Remove(itemCart);

            }
            Itens.Add(item);
            CalcularValorCarrinho();
        }
        internal void UpdateItem(ItemCart item)
        {

            item.AssociarCarrinho(Id);

            var itemExistente = GetItemByIdProduct(item.IdProduct);

            Itens.Remove(itemExistente);

            Itens.Add(item);

            CalcularValorCarrinho();
        }
        internal void UpdateUnit(ItemCart item, int unit)
        {
            item.UpdateUnit(unit);
            UpdateItem(item);
        }
        internal void RemoveItem(ItemCart item)
        {
            var itemExists = GetItemByIdProduct(item.IdProduct);


            Itens.Remove(itemExists);

            CalcularValorCarrinho();

        }

        internal bool IsValid()
        {
            var erros = Itens.SelectMany(i => new ItemCart.ItemValidation().Validate(i).Errors).ToList();
            erros.AddRange(new ClientCartValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(erros);
            return ValidationResult.IsValid;
        }
        public class ClientCartValidation : AbstractValidator<ClientCart>
        {
            public ClientCartValidation()
            {
                RuleFor(c => c.IdClient)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Cliente não reconhecido.");

                RuleFor(c => c.Itens.Count)
                    .GreaterThan(0)
                    .WithMessage("O Carrinho não possui itens.");

                RuleFor(c => c.Total)
                    .GreaterThan(0)
                    .WithMessage("O valor total do carrinho precisa ser maior que 0.");
            }
        }
    }
}
