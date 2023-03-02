using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.ViewModels;
using System.Runtime.Serialization;

namespace Restaurant.Features.OrderMasters
{
    public class Update
    {
        public class Command : OrderMasterViewModel, IRequest<Result>
        {

            [IgnoreDataMember]
            public int Id { get; set; }
            public Command(int id)
            {

                Id = id;

            }
        }
            public class CommandHandler : IRequestHandler<Command, Result>
            {
                private readonly IMapper mapper;
                private readonly RestaurantDbContext context;

                public CommandHandler(IMapper mapper, RestaurantDbContext context)
                {
                    this.mapper = mapper;
                    this.context = context;
                }
                public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var orderMaster = await context.OrderMasters.Include(x=>x.OrderDetails).FirstOrDefaultAsync(x=>x.Id == request.Id);

                if (orderMaster is not null)
                    {
                        orderMaster.PaymentMethod = request.PaymentMethod;

                        context.OrderMasters.Update(orderMaster);
                        await context.SaveChangesAsync(cancellationToken);

                    foreach(var foodRequested in request.OrderDetails)
                    {
                        var orderDetail = "check";
                    }
                    }
                    return Result.Success();
                }

            }
        
    }
}
