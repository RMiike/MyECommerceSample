using EasyNetQ;
using FluentValidation.Results;
using MECS.Client.API.Application.Commands;
using MECS.Core.Data.Mediator;
using MECS.Core.Data.Messages.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MECS.Client.API.Services
{
    public class RegisterClientIntegrationHandler : BackgroundService
    {
        private IBus _bus;

        private readonly IServiceProvider _provider;

        public RegisterClientIntegrationHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        public RegisterClientIntegrationHandler()
        {
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost");

            _bus.Rpc.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
                new ResponseMessage(await RegisterClient(request)));

            return Task.CompletedTask;
        }
        private async Task<ValidationResult> RegisterClient(UserRegisteredIntegrationEvent message)
        {
            var clientCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.CPF);
            ValidationResult success;
            using (var scope = _provider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.SendCommand(clientCommand);
            }
            return success;
        }
    }
}
