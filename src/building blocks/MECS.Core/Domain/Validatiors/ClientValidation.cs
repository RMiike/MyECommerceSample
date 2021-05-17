using FluentValidation;
using MECS.Core.Domain.Entities;
using System;

namespace MECS.Core.Domain.Validatiors
{
    public class ClientValidation : AbstractValidator<Client>
    {
        public ClientValidation()
        {
            const int MAX_NAME_LENGTH = 200;
            const int MIN_NAME_LENGTH = 3;

            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");

            RuleFor(x => x.Name)
                .Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH)
                .WithMessage($"Nome deve ter entre {MIN_NAME_LENGTH} e {MAX_NAME_LENGTH} caracteres.");

            RuleFor(x => x.CPF)
                .SetValidator(new CPFValidation())
                .WithMessage("Campo CPF obrigatório");

            RuleFor(x => x.Email)
                .SetValidator(new EmailValidation())
                .WithMessage("Campo Email obrigatório.");
        }
    }
}
