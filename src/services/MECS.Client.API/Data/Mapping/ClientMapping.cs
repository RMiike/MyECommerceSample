using MECS.Core.Domain.DomainObjects;
using MECS.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MECS.Client.API.Data.Mapping
{
    public class ClientMapping : IEntityTypeConfiguration<Core.Domain.Entities.Client>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.Entities.Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.CPF, tf =>
            {
                tf.Property(c => c.Numero)
                .IsRequired()
                .HasColumnName("CPF")
                .HasColumnType($"varchar({CPF.CPF_LENGTH})");

                tf.Ignore(c => c.ErrorMessages);
                tf.Ignore(c => c.ValidationResult);
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Endereco)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.MAX_LENGTH})");

                tf.Ignore(c => c.ErrorMessages);
                tf.Ignore(c => c.ValidationResult);

            });

            builder.HasOne(c => c.Address)
                .WithOne(c => c.Client)
                .HasForeignKey<Address>(c => c.IdClient);

            builder.Ignore(c => c.ErrorMessages);
            builder.Ignore(c => c.ValidationResult);

        }
    }
}
