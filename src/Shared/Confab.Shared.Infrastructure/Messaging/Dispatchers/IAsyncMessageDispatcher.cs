using Confab.Shared.Abstractions.Messaging;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers;

internal interface IAsyncMessageDispatcher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;
}