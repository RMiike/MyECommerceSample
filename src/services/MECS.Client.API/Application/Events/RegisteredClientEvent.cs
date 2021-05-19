using MECS.Core.Data.Messages;
using System;

namespace MECS.Client.API.Application.Events
{
    public class RegisteredClientEvent : Event
    {
        public RegisteredClientEvent(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            CPF = cpf;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
    }
}
