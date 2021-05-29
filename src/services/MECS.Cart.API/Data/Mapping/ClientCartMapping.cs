using MECS.Cart.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MECS.Cart.API.Data.Mapping
{
    public class ClientCartMapping : IEntityTypeConfiguration<ClientCart>
    {
        public void Configure(EntityTypeBuilder<ClientCart> builder)
        {
            builder.ToTable("ClientCart");
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.IdClient)
                .HasDatabaseName("IDX_Client");

            builder.HasMany(c => c.Itens)
                .WithOne(i => i.ClientCart)
                .HasForeignKey(c => c.IdCart);
        }
    }
}
