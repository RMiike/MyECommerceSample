using MECS.Cart.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MECS.Cart.API.Data
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> opt) : base(opt)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
    
        }
        public DbSet<ItemCart> ItensCart { get; set; }
        public DbSet<ClientCart> ClientCart { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartContext).Assembly);


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
