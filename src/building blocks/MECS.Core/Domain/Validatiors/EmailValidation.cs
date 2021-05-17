using FluentValidation;
using MECS.Core.Domain.DomainObjects;
using System.Text.RegularExpressions;

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
                .WithMessage("Digite um email válido.")
                .Must(CustomValidate)
                .WithMessage("E-mail inválido");
        }
        private bool CustomValidate(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
