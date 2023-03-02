using AutoMapper;
using Restaurant.Domain;

namespace Restaurant.Features.Customers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Customer>();
        }
    }
}
