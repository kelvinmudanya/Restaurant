using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Context;

namespace Restaurant.Data.Mappings
{
    public class OrderDetailMappingIEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}