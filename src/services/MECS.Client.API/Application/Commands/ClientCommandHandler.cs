using FluentValidation.Results;
using MECS.Client.API.Application.Events;
using MECS.Client.API.Interfaces;
using MECS.Core.Data.Messages;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MECS.Client.API.Application.Commands
{
    public class ClientCommandHandler :
        CommandHandler,
        IRequestHandler<RegisterClientCommand, ValidationResult>
    {

        private readonly IClientRepository _repository;

        public ClientCommandHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            var client = new Core.Domain.Entities.Client(message.Id, message.Name, message.Email, message.CPF);

            var clientExists = await _repository.GetByCPF(client.CPF.Numero);

            if (clientExists != null)
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }

            _repository.Adicionar(client);

            client.AddEvent(new RegisteredClientEvent(message.Id, message.Name, message.Email, message.CPF));

            return await PersistData(_repository.UnitOfWork);
        }

    }
}
