using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Attendances.Api.Controllers;

[Microsoft.AspNetCore.Components.Route(AttendancesModule.BasePath)]
internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => "Attendances API";
}