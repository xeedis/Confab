using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

[Route(BasePath + "/[controller]")]
internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => "Conferences API";
}