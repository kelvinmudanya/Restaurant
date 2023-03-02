using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;

namespace Restaurant.Features.FoodItems
{
    public class Details
    {
        public class Query : IRequest<Result<FoodItemViewModel>>
        {
            public int Id { get; set; }
            public Query(int id)
            {
                Id = id;
            }
        }
        public class FoodItemViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }

        }

        public class QueryHandler : IRequestHandler<Query, Result<FoodItemViewModel>>
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

            public async Task<Result<FoodItemViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var foodItem = await context.FoodItems.AsNoTracking()
                    .ProjectTo<FoodItemViewModel>(provider)
                    .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

                return Result.Success(foodItem);
            }
        }
    }
}