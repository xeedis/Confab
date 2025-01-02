using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers;

[ApiController]
[Route(SpeakersModule.BasePath + "/[controller]")]
internal class BaseController : ControllerBase
{
    protected const string BasePath = "speakers-module";

    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}
