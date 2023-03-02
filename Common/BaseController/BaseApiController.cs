using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Common.BaseController
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
#pragma warning disable 649
        private ISender mediator;
#pragma warning restore 649

#pragma warning disable CS8603 // Possible null reference return.
        protected ISender Mediator => HttpContext.RequestServices.GetService<ISender>();
#pragma warning restore CS8603 // Possible null reference return.


        protected IActionResult OkWithActionFeedback(ActionPerformed action) =>
            action switch
            {
                ActionPerformed.Add => Ok(new { message = "Record added successfully" }),
                ActionPerformed.Delete => Ok(new { message = "Record deleted successfully" }),
                ActionPerformed.Update => Ok(new { message = "Record updated successfully" }),
                ActionPerformed.Upload => Ok(new { message = "File uploaded successfully" }),
                _ => BadRequest()
            };

        protected IActionResult OkWithCustomFeedback(string feedback) => string.IsNullOrWhiteSpace(feedback) ? (IActionResult)BadRequest() : Ok(new { message = feedback });

        protected IActionResult RecordNotFound() => NotFound(new { message = "Sorry, that record no longer exists" });

        protected IActionResult BadRequestWithMessage(string message)
        {
            ModelState.AddModelError("generic", message);
            return BadRequest(ModelState);
        }

        protected IActionResult Feedback(Result result, ActionPerformed action) => result.Finally(x => x.IsSuccess ? OkWithActionFeedback(action) : BadRequestWithMessage(x.Error));

        protected IActionResult Feedback(Result result, string feedback) => result.Finally(x => x.IsSuccess ? OkWithCustomFeedback(feedback) : BadRequestWithMessage(x.Error));

        protected IActionResult Process<T>(Result<T> result) => result.Finally(x => x.IsSuccess ? Ok(result.Value) : BadRequestWithMessage(x.Error));
    }
}