using FluentValidation.Results;
using MECS.Core.Data.Messages;
using MediatR;
using System.Threading.Tasks;

namespace MECS.Core.Data.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {

        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T eventHandler) where T : Event
        {
            await _mediator.Publish(eventHandler);
        }
        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
            => await _mediator.Send(command);

    }
}
