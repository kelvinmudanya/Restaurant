using Microsoft.AspNetCore.Mvc;
using Restaurant.Common.BaseController;
using Restaurant.ViewModels;

namespace Restaurant.Features.Customers
{
    public class  CustomersController:BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CustomerViewModel viewModel)
        {
            return Feedback(await Mediator.Send(new Create.Command(viewModel)), ActionPerformed.Add);
        }
    }
}
