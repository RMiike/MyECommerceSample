using FluentValidation;
using MECS.Core.Domain.DomainObjects;

namespace MECS.Core.Domain.Validatiors
{
    public class EmailValidation : AbstractValidator<Email>
    {
        public EmailValidation()
        {
            const int MAX_LENGTH = 250;
            const int MIN_LENGTH = 5;
            RuleFor(x => x.Endereco)
                .Length(MIN_LENGTH, MAX_LENGTH)
                .WithMessage($"Endereço de email deve ter entre {MIN_LENGTH} e {MAX_LENGTH} caracteres.")
                .EmailAddress()
                .WithMessage("Digite um email válido.");


        }

    }
}
