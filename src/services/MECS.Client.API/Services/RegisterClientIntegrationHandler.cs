using FluentValidation.Results;
using MECS.Client.API.Application.Commands;
using MECS.Core.Data.Mediator;
using MECS.Core.Data.Messages.Integration;
using MECS.MessageBus.Bus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MECS.Client.API.Services
{
    public class RegisterClientIntegrationHandler : BackgroundService
    {
        private IMessageBus _bus;

        private readonly IServiceProvider _provider;

        public RegisterClientIntegrationHandler(
            IServiceProvider provider,
            IMessageBus bus)
        {
            _provider = provider;
            _bus = bus;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
               await RegisterClient(request));
            _bus.AdvancedBus.Connected += OnConnect;
        }
        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }
        private async Task<ResponseMessage> RegisterClient(UserRegisteredIntegrationEvent message)
        {
            var clientCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.CPF);
            ValidationResult success;
            using (var scope = _provider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.SendCommand(clientCommand);
            }
            return new ResponseMessage(success);
        }
    }
}
