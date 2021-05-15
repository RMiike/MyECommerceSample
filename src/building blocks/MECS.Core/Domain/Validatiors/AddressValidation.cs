using FluentValidation;
using MECS.Core.Domain.Entities;

namespace MECS.Core.Domain.Validatiors
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            const int LOGRADOURO_LENGTH = 200;
            const int NUMERO_LENGTH = 50;
            const int CEP_LENGTH = 20;
            const int COMPLEMENTO_LENGTH = 250;
            const int BAIRRO_LENGTH = 100;
            const int CIDADE_LENGTH = 100;
            const int ESTADO_LENGTH = 50;

            RuleFor(x => x.Logradouro)
                .Length(LOGRADOURO_LENGTH)
                .WithMessage($"Campo Logradouro deve ter {LOGRADOURO_LENGTH} caracteres.");

            RuleFor(x => x.Numero)
                .Length(NUMERO_LENGTH)
                .WithMessage($"Campo Numero deve ter {NUMERO_LENGTH} caracteres.");

            RuleFor(x => x.CEP)
                .Length(CEP_LENGTH)
                .WithMessage($"Campo CEP deve ter {CEP_LENGTH} caracteres.");

            RuleFor(x => x.Complemento)
                .Length(COMPLEMENTO_LENGTH)
                .WithMessage($"Campo Complemento deve ter {COMPLEMENTO_LENGTH} caracteres.");

            RuleFor(x => x.Bairro)
                .Length(BAIRRO_LENGTH)
                .WithMessage($"Campo Bairro deve ter {BAIRRO_LENGTH} caracteres.");

            RuleFor(x => x.Cidade)
            .Length(CIDADE_LENGTH)
            .WithMessage($"Campo Cidade deve ter {CIDADE_LENGTH} caracteres.");

            RuleFor(x => x.Estado)
            .Length(ESTADO_LENGTH)
            .WithMessage($"Campo Estado deve ter {ESTADO_LENGTH} caracteres.");
        }
    }
}

