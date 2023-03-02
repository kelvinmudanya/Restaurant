using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Domain.Context;
using Restaurant.ViewModels;
using System.Xml.Linq;

namespace Restaurant.Features.OrderMasters
{
    public class Create
    {
        public class Command : OrderMasterViewModel, IRequest<Result>
        {
            public Command(int id, OrderMasterViewModel viewModel)
            {
                CustomerId = id;    
                PaymentMethod= viewModel.PaymentMethod;
                OrderDetails= viewModel.OrderDetails;
            }
            public int CustomerId { get; set; }
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
                    var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId);
                    if(customer is not null)

                    {
                        var orderMaster = new OrderMaster()
                        {
                            OrderNumber = $"DateTime.UtcNow {customer.Name + customer.PhoneNumber}",
                            PaymentMethod = request.PaymentMethod,
                            OrderDetails = new List<OrderDetail>(),
                            Customer = customer
                        };
                        await context.AddAsync(orderMaster);
                        await context.SaveChangesAsync();   

                        foreach(var foodRequested in request.OrderDetails)
                        {
                            var foodItem = await context.FoodItems.FirstOrDefaultAsync(x => x.Id == foodRequested.FoodItemId);
                            if(foodItem is not null)
                            {
                                var orderDetail = new OrderDetail()
                                {
                                    FoodItem = foodItem,
                                    OrderMaster= orderMaster,
                                    FoodItemPrice= foodRequested.FoodItemPrice??foodItem.Price,
                                    Quantity= foodRequested.Quantity,

                                };
                                await context.AddAsync(orderDetail);
                                await context.SaveChangesAsync();

                                orderMaster.GTotal = +orderDetail.FoodItemPrice*orderDetail.Quantity;
                                context.Update(orderMaster);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
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
