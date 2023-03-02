using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Domain.Context;
using Restaurant.Domain.DTOs;
using System.Runtime.Serialization;

namespace Restaurant.Features.OrderMasters
{
    public class Details
    {
        public class Query : IRequest<Result<OrderMasterViewModel>>
        {
            public int Id { get; set; }
            public Query(int id)
            {
                Id = id;
            }
        }
        public class OrderMasterViewModel
        {
            [IgnoreDataMember]
            public int Id { get; set; }
            public string OrderNumber { get; set; }
            public decimal GTotal { get; set; }
            public List<OrderDetailViewDto> OrderDetails { get; set; } = new();
            public string PaymentMethod { get; set; }

        }

        public class QueryHandler : IRequestHandler<Query, Result<OrderMasterViewModel>>
        {
            private readonly IMapper mapper;
            private readonly RestaurantDbContext context;
            private readonly AutoMapper.IConfigurationProvider provider;

            public QueryHandler(IMapper mapper,
                RestaurantDbContext context, AutoMapper.IConfigurationProvider provider)
            {
                this.mapper = mapper;
                this.context = context;
                this.provider = provider;
            }

            public async Task<Result<OrderMasterViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var customer = await context.OrderMasters.Include(x=>x.OrderDetails).ThenInclude(x=>x.FoodItem)
                    .ProjectTo<OrderMasterViewModel>(provider)
                    .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

                return Result.Success(customer);
            }
        }
    }
}