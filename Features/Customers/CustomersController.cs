﻿using Microsoft.AspNetCore.Mvc;
using Restaurant.Common.BaseController;
using Restaurant.Common.Behaviours.Extensions.Paging;
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

        [HttpGet("Requests")]
        public async Task<ActionResult<PagedResult<List.CustomerViewModel>>> GetRequests(int? page, string? q)
        {
            return await Mediator.Send(new List.Query(page, q));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Update.Command command)
        {
            command.Id = id;
            return Feedback(await Mediator.Send(command), ActionPerformed.Update);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, Delete.Command command)
        {
            command.Id = id;
            return Feedback(await Mediator.Send(command), ActionPerformed.Delete);
        }
    }

}
