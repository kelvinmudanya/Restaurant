using CSharpFunctionalExtensions;
using MediatR;
using Restaurant.Data;
using System.Runtime.Serialization;

namespace Restaurant.Features.FoodItems
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
                var foodItem = await context.FoodItems.FindAsync(request.Id);
                if (foodItem is not null)
                {
                    context.FoodItems.Remove(foodItem);
                    await context.SaveChangesAsync(cancellationToken);

                }


                return Result.Success();
            }

        }

    }
}