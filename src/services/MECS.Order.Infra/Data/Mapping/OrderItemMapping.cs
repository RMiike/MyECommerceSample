using MECS.Order.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MECS.Order.Infra.Data.Mapping
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(o => o.ProductName)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.HasOne(o => o.Order)
                .WithMany(o => o.OrderItems);


            builder.ToTable("OrderItem");
        }
    }
}
