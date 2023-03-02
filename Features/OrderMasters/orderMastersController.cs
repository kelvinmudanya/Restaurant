using Microsoft.AspNetCore.Mvc;
using Restaurant.Common.BaseController;
using Restaurant.ViewModels;

namespace Restaurant.Features.OrderMasters
{
    public class orderMastersController:BaseApiController
    {
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, OrderMasterViewModel viewModel)
        {
            return Feedback(await Mediator.Send(new Create.Command(id, viewModel)), ActionPerformed.Add);
        }

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


    }
}
