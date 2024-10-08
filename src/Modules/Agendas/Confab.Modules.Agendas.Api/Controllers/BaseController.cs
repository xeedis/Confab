using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route(AgendasModule.BasePath + "/[controller]")]
internal class BaseController : ControllerBase
{
    protected const string BasePath = "agendas-module";

    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}