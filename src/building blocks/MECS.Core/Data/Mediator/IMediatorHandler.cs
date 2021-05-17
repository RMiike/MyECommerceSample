using FluentValidation.Results;
using MECS.Core.Data.Messages;
using System.Threading.Tasks;

namespace MECS.Core.Data.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T eventHandler) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;

    }
}
