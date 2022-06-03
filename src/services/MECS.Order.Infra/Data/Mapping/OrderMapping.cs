using MECS.Order.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MECS.Order.Infra.Data.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(p => p.Address, e =>
            {
                e.Property(c => c.Logradouro)
                .HasColumnName("Logradouro")
                .HasColumnType("varchar(200)");

                e.Property(c => c.Numero)
                .HasColumnName("Numero")
                .HasColumnType("varchar(50)");

                e.Property(c => c.CEP)
                .HasColumnName("CEP")
                .HasColumnType("varchar(20)");

                e.Property(c => c.Complemento)
                .HasColumnName("Complemento")
                .HasColumnType("varchar(250)");

                e.Property(c => c.Bairro)
                .HasColumnName("Bairro")
                .HasColumnType("varchar(100)");

                e.Property(c => c.Cidade)
                .HasColumnName("Cidade")
                .HasColumnType("varchar(100)");

                e.Property(c => c.Estado)
                .HasColumnName("Estado")
                .HasColumnType("varchar(50)");
            });

            builder.Property(o => o.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.HasMany(c => c.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.IdOrder);

            builder.ToTable("Order");
        }
    }
}
