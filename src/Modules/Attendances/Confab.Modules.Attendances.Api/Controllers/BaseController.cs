using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Attendances.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route(AttendancesModule.BasePath + "/[controller]")]
internal abstract class BaseController : ControllerBase
{
}