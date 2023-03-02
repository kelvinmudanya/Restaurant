using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Context;

namespace Restaurant.Data.Mappings
{
    public class FoodItemMapping: IEntityTypeConfiguration<FoodItem>
    {
        public void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
