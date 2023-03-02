using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Restaurant.Data;
using Restaurant.Domain.Context;
using Restaurant.ViewModels;

namespace Restaurant.Features.FoodItems
{
    public class Create
    {
        public class Command : FoodItemViewModel, IRequest<Result>
        {
            public Command(FoodItemViewModel viewModel)
            {
                Name = viewModel.Name;
                Price = viewModel.Price;

            }
        }

        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly RestaurantDbContext context;
            private readonly IMapper mapper;


            public CommandHandler(RestaurantDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var foodItem = mapper.Map<FoodItem>(request);

                    await context.FoodItems.AddAsync(foodItem, cancellationToken);
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
