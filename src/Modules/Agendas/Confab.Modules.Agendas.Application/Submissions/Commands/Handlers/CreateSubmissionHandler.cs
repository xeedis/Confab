using Confab.Modules.Agendas.Application.CallForPapers.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Services;
using Confab.Modules.Agendas.Domain.CallForPapers.Repositories;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers;

public sealed class CreateSubmissionHandler : ICommandHandler<CreateSubmission>
{
    private readonly ISubmissionRepository _submissionRepository;
    private readonly ISpeakerRepository _speakerRepository;
    private readonly ICallForPapersRepository _callForPapersRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public CreateSubmissionHandler(ISubmissionRepository submissionRepository, 
        ISpeakerRepository speakerRepository, IDomainEventDispatcher domainEventDispatcher, 
        IMessageBroker messageBroker, IEventMapper eventMapper, ICallForPapersRepository callForPapersRepository)
    {
        _submissionRepository = submissionRepository;
        _speakerRepository = speakerRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
        _callForPapersRepository = callForPapersRepository;
    }

    public async Task HandleAsync(CreateSubmission command)
    {
        var callForPapers = await _callForPapersRepository.GetAsync(command.ConferenceId);
        if (callForPapers is null)
        {
            throw new CallForPapersNotFoundException(command.ConferenceId);
        }

        if (!callForPapers.IsOpened)
        {
            throw new CallForPapersClosedException(command.ConferenceId);
        }
        
        var speakerIds = command.SpeakerIds.Select(x => new AggregateId(x));
        var speakers = await _speakerRepository.BrowseAsync(speakerIds);
        

        if (speakers.Count() != speakerIds.Count())
        {
            throw new InvalidSpeakerNumberException(command.Id);
        }

        var submission = Submission.Create(command.Id,command.ConferenceId, command.Title,
            command.Description, command.Level, command.Tags, speakers.ToList());
        
        await _submissionRepository.AddAsync(submission);
        await _domainEventDispatcher.DispatchAsync(submission.Events.ToArray());
        
        var integrationEvents = _eventMapper.MapAll(submission.Events);
        await _messageBroker.PublishAsync(integrationEvents.ToArray());
    }
}