using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers;

internal class SubmissionsController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    
    public SubmissionsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SubmissionDto>> GetAsync(Guid id)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetSubmission { Id = id }));
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateSubmission command)
    {
        await _commandDispatcher.SendAsync(command);
        return CreatedAtAction("Get", new { id = command.Id }, null);
    }
    
    [HttpPut("{id:guid}/approve")]
    public async Task<ActionResult> ApproveAsync(Guid id)
    {
        await _commandDispatcher.SendAsync(new ApproveSubmission(id));
        return NoContent();
    }
    
    [HttpPut("{id:guid}/reject")]
    public async Task<ActionResult> RejectAsync(Guid id)
    {
        await _commandDispatcher.SendAsync(new RejectSubmission(id));
        return NoContent();
    }
}