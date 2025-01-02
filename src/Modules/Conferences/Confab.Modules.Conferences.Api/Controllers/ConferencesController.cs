using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

[Authorize(Policy = Policy)]
internal class ConferencesController(IConferenceService conferenceService) : BaseController
{
    private const string Policy = "conferences";
    private readonly IConferenceService _conferenceService = conferenceService;

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConferenceDetailsDto>> Get(Guid id)
        => OkOrNotFound(await _conferenceService.GetAsync(id));
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ConferenceDto>>> BrowseAsync()
        => Ok(await _conferenceService.BrowseAsync());

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddAsync(ConferenceDetailsDto dto)
    {
        await _conferenceService.AddAsync(dto);

        return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateAsync(Guid id, ConferenceDetailsDto dto)
    {
        dto.Id = id;
        await _conferenceService.UpdateAsync(dto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _conferenceService.DeleteAsync(id);
        return NoContent();
    }
}