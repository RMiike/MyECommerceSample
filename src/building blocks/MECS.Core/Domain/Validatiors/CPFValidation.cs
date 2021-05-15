﻿using FluentValidation;
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
                .Must(c => ValidarTamanho(c))
                .Length(CPF_LENGTH)
                .WithMessage($"CPF deve ter {CPF_LENGTH} caracteres.")
                .Must(c => ValidarCPFCustomizada(c))
                .WithMessage("CPF inválido.");
        }
        private bool ValidarTamanho(string cpf)
        {
            cpf = cpf.OnlyNumbers(cpf);

            if (cpf.Length > 11)
                return false;

            return true;
        }
        private bool ValidarCPFCustomizada(string cpf)
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