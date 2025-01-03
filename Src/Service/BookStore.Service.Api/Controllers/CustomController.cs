using BookStore.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Service.Api.Controllers
{
    [ApiController]
    public class CustomController : ControllerBase
    {
        protected bool HasNotifications(Response response)
        {
            return response.Notifications.Count != 0;
        }

        protected bool NoData(Response response)
        {
            return response.Data == null;
        }

        protected IActionResult BadRequest(Response response)
        {
            return BadRequest(new
            {
                notifications = response.Notifications.Select(x => x.Message)
            });
        }

        protected IActionResult Ok(Response response)
        {
            return Ok(response.Data);
        }
    }
}
