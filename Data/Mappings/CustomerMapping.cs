using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Context;

namespace Restaurant.Data.Mappings
{
    public class CustomerMapping: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
