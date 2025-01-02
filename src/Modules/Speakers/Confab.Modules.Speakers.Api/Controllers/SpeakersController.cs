using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers;
[Authorize(Policy = Policy)]
internal class SpeakersController(ISpeakersService speakersService) : BaseController
{
    private const string Policy = "speakers";
    private readonly ISpeakersService _speakersService = speakersService;

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SpeakerDto>> Get(Guid id)
        => OkOrNotFound(await _speakersService.GetAsync(id));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpeakerDto>>> BrowseAsync()
        => Ok(await _speakersService.BrowseAsync());

    [HttpPost]
    public async Task<ActionResult<SpeakerDto>> AddAsync(SpeakerDto dto)
    {
        await _speakersService.CreateAsync(dto);
        
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SpeakerDto>> Update(Guid id, SpeakerDto dto)
    {
        dto.Id = id;
        await _speakersService.UpdateAsync(dto);
        return NoContent();
    }
}
        