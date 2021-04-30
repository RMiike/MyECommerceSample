using FluentValidation;
using MECS.Identity.API.Models.BaseModel;

namespace MECS.Identity.API.Models
{
    public class SignUpUser : BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new SignUpUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        public class SignUpUserValidation : AbstractValidator<SignUpUser>
        {
            private readonly int PASSWORD_MAX_LENGTH = 100;
            private readonly int PASSWORD_MIN_LENGTH = 6;
            public SignUpUserValidation()
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
                    .WithMessage($"O Campo Password deve ter entre {PASSWORD_MIN_LENGTH} e {PASSWORD_MAX_LENGTH} caracteres.");
                ;
                RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.Password)
                    .WithMessage("As senhas não conferem.");
            }
        }
    }
    public class SignInUser : BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new SignInUserUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
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
                    .WithMessage($"O Campo Password deve ter entre {PASSWORD_MIN_LENGTH} e {PASSWORD_MAX_LENGTH} caracteres.");
            }
        }
    }
}
