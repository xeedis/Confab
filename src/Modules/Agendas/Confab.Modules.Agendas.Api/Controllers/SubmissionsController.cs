using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers;

internal class SubmissionsController : BaseController
{
    private readonly ICommandDispatcher _commmandDispatcher;

    public SubmissionsController(ICommandDispatcher commmandDispatcher)
    {
        _commmandDispatcher = commmandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateSubmission command)
    {
        await _commmandDispatcher.SendAsync(command);
        return CreatedAtAction("Get", new { id = command.Id }, null);
    }
    
    [HttpPut("{id:guid}/approve")]
    public async Task<ActionResult> ApproveAsync(Guid id)
    {
        await _commmandDispatcher.SendAsync(new ApproveSubmission(id));
        return NoContent();
    }
    
    [HttpPut("{id:guid}/reject")]
    public async Task<ActionResult> RejectAsync(Guid id)
    {
        await _commmandDispatcher.SendAsync(new RejectSubmission(id));
        return NoContent();
    }
}