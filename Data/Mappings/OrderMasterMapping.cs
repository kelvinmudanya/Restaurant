using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain;

namespace Restaurant.Data.Mappings
{
    public class OrderMasterMapping : IEntityTypeConfiguration<OrderMaster>
    {
        public void Configure(EntityTypeBuilder<OrderMaster> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderNumber).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PaymentMethod).HasMaxLength(300);
        }
    }
}
