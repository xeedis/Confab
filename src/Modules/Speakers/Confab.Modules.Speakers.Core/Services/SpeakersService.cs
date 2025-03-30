using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DAL;
using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Events;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Mappers;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Speakers.Core.Services;

internal sealed class SpeakersService(ISpeakersRepository speakersRepository, SpeakersDbContext context, IMessageBroker messageBroker)
    : ISpeakersService
{
    private readonly ISpeakersRepository _speakersRepository = speakersRepository;
    private readonly SpeakersDbContext _context = context;
    private readonly IMessageBroker _messageBroker = messageBroker;

    public async Task<IEnumerable<SpeakerDto>> BrowseAsync()
    {
        var entities =  await _speakersRepository.BrowseAsync();
        return entities?.Select(x => x.AsDto());
    }

    public async Task<SpeakerDto> GetAsync(Guid id)
    {
        var entity = await _speakersRepository.GetAsync(id);
        return entity?.AsDto();
    }

    public async Task CreateAsync(SpeakerDto dto)
    {
        var alreadyExists = await _speakersRepository.ExistsAsync(dto.Id);

        if (alreadyExists)
        {
            throw new SpeakerAlreadyExistsException(dto.Id);
        }
        
        await _speakersRepository.AddAsync(dto.AsEntity());
        await _messageBroker.PublishAsync(new SpeakerCreated(dto.Id, dto.FullName));
    }

    public async Task UpdateAsync(SpeakerDto dto)
    {
        var exists = await _speakersRepository.ExistsAsync(dto.Id);
        
        if (!exists)
        {
            throw new SpeakerNotFoundException(dto.Id);
        }
        
        await _speakersRepository.UpdateAsync(dto.AsEntity());
    }
}