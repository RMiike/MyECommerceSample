using FluentValidation.Results;
using MECS.Core.Data.Interface;
using MECS.Core.Data.Mediator;
using MECS.Core.Data.Messages;
using MECS.Order.Domain.Vouchers;
using MECS.Order.Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MECS.Order.Infra.Data
{
    public class OrderContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public OrderContext(
            DbContextOptions<OrderContext> opt,
            IMediatorHandler mediatorHandler)
            : base(opt)
        {
            _mediatorHandler = mediatorHandler;
        }
        public DbSet<Voucher> Vouchers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;
            if (success)
                await _mediatorHandler.PublishEvents(this);

            return success;
        }
    }
}
