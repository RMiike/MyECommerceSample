using FluentValidation;
using MECS.Core.Domain.Entities;

namespace MECS.Core.Domain.Validatiors
{
    public class SignInUserUserValidation : AbstractValidator<SignInUser>
    {
        private readonly int PASSWORD_MAX_LENGTH = 100;
        private readonly int PASSWORD_MIN_LENGTH = 6;
        public SignInUserUserValidation()
        {
            RuleFor(x => x.Email)
                 .NotNull()
                 .WithMessage("O campo Email é obrigatório.")
                 .EmailAddress()
                 .WithMessage("O campo Email está em formato inválido.");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("O campo Password é obrigatório.")
                .Length(PASSWORD_MIN_LENGTH, PASSWORD_MAX_LENGTH)
                .WithMessage($"O Campo Password deve ter entre {PASSWORD_MIN_LENGTH} e {PASSWORD_MAX_LENGTH} caracteres.")
                 .Length(PASSWORD_MIN_LENGTH, PASSWORD_MAX_LENGTH)
                .WithMessage($"O Campo Password deve ter entre {PASSWORD_MIN_LENGTH} e {PASSWORD_MAX_LENGTH} caracteres.");
        }
    }
}
