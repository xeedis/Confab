using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events.External.Handlers;

public class SpeakerCreatedHandler : IEventHandler<SpeakerCreated>
{
    private readonly  ISpeakerRepository _repository;

    public SpeakerCreatedHandler(ISpeakerRepository repository)
        => _repository = repository;
    

    public async Task HandleAsync(SpeakerCreated @event)
    {
        if (await _repository.ExistsAsync(@event.Id))
        {
            return;
        }

        var speaker = Speaker.Create(@event.Id, @event.FullName);
        await _repository.CreateAsync(speaker);
    }
}