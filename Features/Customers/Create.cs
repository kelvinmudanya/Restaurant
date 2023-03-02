using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Restaurant.ViewModels;
using Restaurant.Data;
using Restaurant.Domain;

namespace Restaurant.Features.Customers
{
    public class Create
    {
        public class Command : CustomerViewModel, IRequest<Result>
        {
            public Command(CustomerViewModel viewModel) 
            {
                Name= viewModel.Name;
                PhoneNumber= viewModel.PhoneNumber;

            }
        }

        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly RestaurantDbContext context;
            private readonly IMapper mapper;


            public CommandHandler(RestaurantDbContext context,IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = mapper.Map<Customer>(request);

                    await context.Customers.AddAsync(customer, cancellationToken);
                    await context.SaveChangesAsync();

                }
                catch (Exception ex)
                {

                    Console.WriteLine("The request could not be send" + ex.Message);
                }

                return Result.Success();
            }
        }
    }
}
