namespace Confab.Shared.Abstractions.Contexts;

public interface IIdentityContext
{
    bool IsAuthenticated { get; }
    public Guid UserId { get; }
    string Role { get; }
    Dictionary<string, IEnumerable<string>> Claims { get; }
}