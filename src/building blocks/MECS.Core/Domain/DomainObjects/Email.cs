using MECS.Core.Domain.Validatiors;
using MECS.Core.Extensions;

namespace MECS.Core.Domain.DomainObjects
{
    public class Email : BaseDomainObject
    {
        protected Email() { }
        public Email(string endereco)
        {
            if (!IsValid())
                throw new DomainException("E-mail inválido.");
            Endereco = endereco;
        }
        public const int MAX_LENGTH = 250;
        public const int MIN_LENGTH = 5;
        public string Endereco { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new EmailValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
