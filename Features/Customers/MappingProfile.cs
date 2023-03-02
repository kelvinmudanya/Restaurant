using AutoMapper;
using Restaurant.Domain.Context;

namespace Restaurant.Features.Customers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Customer>();
            CreateMap<Customer, List.CustomerViewModel>();
            CreateMap<Update.Command, Customer>(); 
            CreateMap<Customer, Details.CustomerViewModel>();   
        }
    }
}
