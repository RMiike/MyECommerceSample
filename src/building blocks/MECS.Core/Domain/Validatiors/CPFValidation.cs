using FluentValidation;
using MECS.Core.Domain.DomainObjects;
using MECS.Core.Extensions;

namespace MECS.Core.Domain.Validatiors
{
    public class CPFValidation : AbstractValidator<CPF>
    {
        public CPFValidation()
        {
            const int CPF_LENGTH = 11;
            RuleFor(x => x.Numero)
                .Length(CPF_LENGTH)
                .WithMessage($"CPF deve ter {CPF_LENGTH} caracteres.");
        }
       
       
    }
}
