using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Restaurant.Data;
using System.Runtime.Serialization;

namespace Restaurant.Features.Customers
{
    public class Delete
    {
        public class Command : IRequest<Result>
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
                private readonly RestaurantDbContext context;


                public CommandHandler(RestaurantDbContext context)
                {
                    this.context = context;
                }
                public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
                {
                    var customer = await context.Customers.FindAsync(request.Id);
                    if (customer is not null)
                    {
                        context.Customers.Remove(customer);
                        await context.SaveChangesAsync(cancellationToken);

                    }


                    return Result.Success();
                }

            }
        
    }
}