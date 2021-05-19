using MECS.Core.Data.Mediator;
using MECS.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MECS.Client.API.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediatorHandler, T context) where T : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(e => e.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediatorHandler.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
