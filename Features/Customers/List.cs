using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Common.Behaviours.Extensions.Paging;
using Restaurant.Data;
using Restaurant.Domain.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Features.Customers
{
    public class List
    {
        public class Query : IRequest<PagedResult<CustomerViewModel>>
        {
            public Query(int? page, string? s)
            {
                Page = page;
                Q = s;
            }

            public int? Page { get; set; }
            public string? Q { get; set; }
        }
        public class CustomerViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }

        }

        public class QueryHandler : IRequestHandler<Query, PagedResult<CustomerViewModel>>
        {
            private readonly AutoMapper.IConfigurationProvider provider;

            private readonly RestaurantDbContext context;

            public QueryHandler(AutoMapper.IConfigurationProvider provider, RestaurantDbContext context)
            {
                this.provider = provider;
                this.context = context;
            }

            public async Task<PagedResult<CustomerViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var pageNumber = request.Page ?? 1;
                var q = request.Q?.ToLower();
                var last24Hrs = DateTime.UtcNow.AddDays(-1);

                var model = context.Customers
                    .AsNoTracking();
                var results = !string.IsNullOrWhiteSpace(q)
                    ? model
                        .Where(x => x.Name == q || EF.Functions.Like(x.Name.ToLower(), $"%{q}%") || EF.Functions.Like(x.PhoneNumber.ToLower(), $"%{q}%"))
                        .OrderByDescending(x => x.Id)
                    : model
                        .OrderBy(x => x.Id);
                        

                return await results.GetPagedAsync<Customer, CustomerViewModel>(provider, pageNumber);

            }
        }
    }
}


