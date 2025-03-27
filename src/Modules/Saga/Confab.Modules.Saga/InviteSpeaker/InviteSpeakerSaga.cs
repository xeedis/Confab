using Chronicle;
using Confab.Modules.Saga.Messages;
using Confab.Shared.Abstractions.Modules;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Saga.InviteSpeaker;

internal class InviteSpeakerSaga : Saga<InviteSpeakerSaga.SagaData>,
    ISagaStartAction<SignedUp>,
    ISagaAction<SpeakerCreated>,
    ISagaAction<SignedIn>
{
    private readonly IModuleClient _moduleClient;

    public InviteSpeakerSaga(IModuleClient moduleClient)
    {
        _moduleClient = moduleClient;
    }
    
    public async Task HandleAsync(SignedUp message, ISagaContext context)
    {
        var (userId, email) = message;
        if (Data.InvitedSpeakers.TryGetValue(email, out var fullName))
        {
            Data.Email = email;
            Data.FullName = fullName;

            await _moduleClient.SendAsync("speakers/create", new
            {
                Id = userId,
                Email = email,
                FullName = fullName,
                Bio = "Lorem Ipsum"
            });
            
            return;
        }

        await CompleteAsync();
    }

    public Task HandleAsync(SpeakerCreated message, ISagaContext context)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(SignedIn message, ISagaContext context)
    {
        throw new NotImplementedException();
    }

    public Task CompensateAsync(SignedUp message, ISagaContext context)
        => Task.CompletedTask;
    
    public Task CompensateAsync(SpeakerCreated message, ISagaContext context)
        => Task.CompletedTask;
    
    public Task CompensateAsync(SignedIn message, ISagaContext context)
        => Task.CompletedTask;
    
    internal class SagaData
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool SpeakerCreated { get; set; }
        
        public readonly Dictionary<string, string> InvitedSpeakers = new()
        {
            ["testSpeaker1@confab.io"] = "John Smith",
            ["testSpeaker2@confab.io"] = "Mark Sim",
        };
    }
}