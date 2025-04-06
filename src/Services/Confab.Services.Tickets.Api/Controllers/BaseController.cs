using Microsoft.AspNetCore.Mvc;

namespace Confab.Services.Tickets.Api.Controllers;

[ApiController]
[Route("[controller]")]
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
