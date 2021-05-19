using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MECS.Client.API.Application.Events
{
    public class ClientEventHandler : INotificationHandler<RegisteredClientEvent>
    {
        public Task Handle(RegisteredClientEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
