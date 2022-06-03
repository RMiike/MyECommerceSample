using FluentValidation;
using MECS.Core.Data.Messages;
using MECS.Order.API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MECS.Order.API.Application.Commands
{
    public class AddOrderCommand : Command
    {
        //Order
        public Guid IdClient { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

        //Voucher
        public string VoucherCodigo { get; set; }
        public bool IsUsedVoucher { get; set; }
        public decimal Descount { get; set; }

        //Address
        public AddressDTO Address { get; set; }

        //Cartao
        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CVVCartao { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        public class AddOrderValidation : AbstractValidator<AddOrderCommand>
        {
            public AddOrderValidation()
            {
                RuleFor(c => c.IdClient)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido.");


                RuleFor(c => c.OrderItems.Count())
                    .GreaterThan(0)
                    .WithMessage("O pedido precisa ter no mínimo 1 item.");

                RuleFor(c => c.Total)
                    .GreaterThan(0)
                    .WithMessage("Valor do pedido inválido");

                RuleFor(c => c.NumeroCartao)
                    .CreditCard()
                    .WithMessage("Número de catão inválido.");

                RuleFor(c => c.NomeCartao)
                    .NotNull()
                    .WithMessage("Nome do portador do cartão inválido.");

                RuleFor(c => c.CVVCartao.Length)
                    .GreaterThan(2)
                    .LessThan(5)
                    .WithMessage("O CVV do cartão precisa ter 3 ou 4 números.");

                RuleFor(c => c.ExpiracaoCartao)
                    .NotNull()
                    .WithMessage("Data de expiração do cartão requerida.");
            }

        }
    }

}
