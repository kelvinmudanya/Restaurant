using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Restaurant.Data;
using Restaurant.ViewModels;
using System.Runtime.Serialization;

namespace Restaurant.Features.FoodItems
{
    public class Update
    {
        public class Command : FoodItemViewModel, IRequest<Result>
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
                    var foodItem = await context.FoodItems.FindAsync(request.Id);
                    if (foodItem is not null)
                    {
                        mapper.Map(request, foodItem);

                        context.FoodItems.Update(foodItem);
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    return Result.Success();
                }
            }
        
    }
}