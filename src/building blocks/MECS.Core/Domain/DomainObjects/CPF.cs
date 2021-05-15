using MECS.Core.Domain.Validatiors;
using MECS.Core.Extensions;

namespace MECS.Core.Domain.DomainObjects
{
    public class CPF : BaseDomainObject
    {
        protected CPF() { }
        public CPF(string numero)
        {
            if (!IsValid())
                throw new DomainException("CPF inválido.");
            Numero = numero;
        }
        public const int CPF_LENGTH = 11;
        public string Numero { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new CPFValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
