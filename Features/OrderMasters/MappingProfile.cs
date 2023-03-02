using AutoMapper;
using Restaurant.Domain.Context;
using Restaurant.Domain.DTOs;
using Restaurant.ViewModels;

namespace Restaurant.Features.OrderMasters
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderMaster, Details.OrderMasterViewModel>();
            CreateMap<OrderDetail, OrderDetailViewDto>();
            CreateMap<FoodItem, FoodItemDto>();
            CreateMap<Update.Command, OrderMasterViewModel>();
            CreateMap<OrderDetailDto, OrderDetail>();
        }
    }
}
