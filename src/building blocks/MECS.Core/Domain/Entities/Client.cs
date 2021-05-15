using MECS.Core.Domain.DomainObjects;
using MECS.Core.Domain.Interfaces;
using MECS.Core.Domain.Validatiors;
using System;

namespace MECS.Core.Domain.Entities
{
    public class Client : BaseEntity, IAggregateRoot
    {
        protected Client() { }
        public Client(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            CPF = new CPF(cpf);
            IsExcluded = false;
        }

        public string Name { get; private set; }
        public Email Email { get; private set; }
        public CPF CPF { get; private set; }
        public bool IsExcluded { get; private set; }
        public Address Address { get; set; }
        public void TrocarEmail(string email)
        {
            Email = new Email(email);
        }
        public void AtribuirEndereco(Address endereco)
        {
            Address = endereco;
        }
        public override bool IsValid()
        {
            ValidationResult = new ClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
