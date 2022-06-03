using FluentValidation.Results;
using MECS.Core.Data.Interface;
using MECS.Core.Data.Mediator;
using MECS.Core.Data.Messages;
using MECS.Order.Domain.Orders;
using MECS.Order.Domain.Vouchers;
using MECS.Order.Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
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
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MinhaSequencia")
                .StartsAt(1000)
                .IncrementsBy(1);

            base.OnModelCreating(modelBuilder);

        }
        public async Task<bool> Commit()
        {

            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("InitialDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("InitialDate").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("InitialDate").IsModified = false;
                }
            }
            var success = await base.SaveChangesAsync() > 0;
            if (success)
                await _mediatorHandler.PublishEvents(this);

            return success;
        }
    }
}
