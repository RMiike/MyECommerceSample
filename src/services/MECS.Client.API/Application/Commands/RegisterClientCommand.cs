using FluentValidation;
using MECS.Core.Data.Messages;
using MECS.Core.Extensions;
using System;
using System.Text.RegularExpressions;

namespace MECS.Client.API.Application.Commands
{
    public class RegisterClientCommand : Command
    {
        public RegisterClientCommand(Guid id,
                                     string name,
                                     string email,
                                     string cPF)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            CPF = cPF;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public override bool IsValid()
        {
            ValidationResult = new RegisterClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class RegisterClientValidation : AbstractValidator<RegisterClientCommand>
    {
        public RegisterClientValidation()
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
                .Must(ValidateCPF)
                .WithMessage("Campo CPF obrigatório");

            RuleFor(x => x.Email)
                .Must(ValidateEmail)
                .WithMessage("Campo Email obrigatório.");
        }

        private bool ValidateEmail(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
        private bool ValidateCPF(string cpf)
        {
            cpf = cpf.OnlyNumbers(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            var igual = true;
            for (var i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            var numeros = new int[11];

            for (var i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            var resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
    }
}
