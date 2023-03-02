using AutoMapper;
using Restaurant.Domain.Context;
using Restaurant.ViewModels;

namespace Restaurant.Features.FoodItems
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, FoodItem>();
            CreateMap<FoodItem, List.FoodItemVIewModel>(); 
            CreateMap<FoodItem, Details.FoodItemViewModel>();
            CreateMap<Update.Command, FoodItem>(); 

        }
    }
}
