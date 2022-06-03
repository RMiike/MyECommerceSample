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
            builder.Ignore(c => c.ValidationResult);
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.IdClient)
                .HasDatabaseName("IDX_Client");

            builder.HasMany(c => c.Itens)
                .WithOne(i => i.ClientCart)
                .HasForeignKey(c => c.IdCart);


            builder.Ignore(c => c.Voucher)
                .OwnsOne(c => c.Voucher, v =>
            {
                v.Property(vc => vc.Codigo)
                    .HasColumnName("VoucherCodigo")
                    .HasColumnType("varchar(50)");

                v.Property(vc => vc.TipoDesconto)
                    .HasColumnName("TipoDesconto");

                v.Property(vc => vc.Percentual)
                    .HasColumnName("Percentual");

                v.Property(vc => vc.ValorDesconto)
                    .HasColumnName("ValorDesconto");
            });
        }
    }
}
