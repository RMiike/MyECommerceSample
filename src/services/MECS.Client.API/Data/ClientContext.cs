using MECS.Client.API.Extensions;
using MECS.Core.Data.Interface;
using MECS.Core.Data.Mediator;
using MECS.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MECS.Client.API.Data
{
    public class ClientContext : DbContext, IUnitOfWork
    {

        private readonly IMediatorHandler _mediatorHandler;
        public ClientContext(DbContextOptions<ClientContext> opt, IMediatorHandler mediatorHandler) : base(opt)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }
        public DbSet<Core.Domain.Entities.Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
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
