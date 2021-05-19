using MECS.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MECS.Client.API.Data.Mapping
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Logradouro)
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Numero)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.CEP)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Complemento)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Bairro)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Cidade)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Estado)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Ignore(c => c.ErrorMessages);
            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.Notifications);
        }
    }
}
