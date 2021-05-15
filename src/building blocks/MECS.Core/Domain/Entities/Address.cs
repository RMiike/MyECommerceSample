using MECS.Core.Domain.Validatiors;
using System;

namespace MECS.Core.Domain.Entities
{
    public class Address : BaseEntity
    {
        protected Address() { }
        public Address(string logradouro,
                       string numero,
                       string complemento,
                       string bairro,
                       string cep,
                       string cidade,
                       string estado,
                       Guid idClient)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            IdClient = idClient;
        }

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid IdClient { get; private set; }
        public Client Client { get; protected set; }
        public override bool IsValid()
        {
            ValidationResult = new AddressValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
