using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Tickets.Api.Controllers;

[ApiController]
[Route(TicketsModule.BasePath + "/[controller]")]
public class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}
