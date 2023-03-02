using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;

namespace Restaurant.Features.Customers
{
    public class Details
    {
        public class Query : IRequest<Result<CustomerViewModel>>
        {
            public int Id { get; set; }
            public Query(int id)
            {
                Id = id;
            }
        }
        public class CustomerViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Result<CustomerViewModel>>
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

            public async Task<Result<CustomerViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var customer = await context.Customers.AsNoTracking()
                    .ProjectTo<CustomerViewModel>(provider)
                    .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

                return Result.Success(customer);
            }
        }
    }
}