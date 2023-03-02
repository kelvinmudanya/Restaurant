using Microsoft.AspNetCore.Mvc;
using Restaurant.Common.BaseController;
using Restaurant.Common.Behaviours.Extensions.Paging;
using Restaurant.ViewModels;

namespace Restaurant.Features.FoodItems
{
    public class FoodItemsController:BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(FoodItemViewModel viewModel)
        {
            return Feedback(await Mediator.Send(new Create.Command(viewModel)), ActionPerformed.Add);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<List.FoodItemVIewModel>>> Get(int? page, string? q) => Ok(await Mediator.Send(new List.Query( page, q)));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new Details.Query(id));
            if (result.IsSuccess)
            {
                if (result.Value != null)
                {
                    return Ok(result.Value);
                }

                return NotFound();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Update.Command command)
        {
            command.Id = id;
            return Feedback(await Mediator.Send(command), ActionPerformed.Update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, Delete.Command command)
        {
            command.Id = id;
            return Feedback(await Mediator.Send(command), ActionPerformed.Delete);
        }

    }
}
