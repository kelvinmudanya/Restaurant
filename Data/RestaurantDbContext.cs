using Microsoft.EntityFrameworkCore;
using Restaurant.Domain;

namespace Restaurant.Data
{
    public class RestaurantDbContext:DbContext
    {
        protected readonly IConfiguration Configuration;
        public RestaurantDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DevConnection"));
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }
    }
}
