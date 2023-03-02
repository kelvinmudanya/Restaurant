using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Restaurant.Data;
using Restaurant.ViewModels;
using System.Runtime.Serialization;

namespace Restaurant.Features.Customers
{
    public class Update
    {
        public class Command : CustomerViewModel, IRequest<Result>
        {

            [IgnoreDataMember]
            public int Id { get; set; }
            public Command(int id)
            {

                Id = id;

            }
        }

        public class Validator : AbstractValidator<Command>
        {
            private readonly RestaurantDbContext context;
            public Validator(RestaurantDbContext context)
            {
                this.context = context;

                RuleFor(x => x.Name)
                 .NotEmpty()
                 .MinimumLength(3);

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
                    var customer = await context.Customers.FindAsync(request.Id);
                    if(customer is not  null)
                    {
                        mapper.Map(request, customer);

                        context.Customers.Update(customer);
                        await context.SaveChangesAsync(cancellationToken);

                    }
                  

                    return Result.Success();
                }

            }
        }
    }
}
