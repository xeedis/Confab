using System.Security.Claims;
using Confab.Shared.Abstractions.Contexts;

namespace Confab.Shared.Infrastructure.Contexts;

internal class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid UserId { get; }
    public string Role { get; }
    public Dictionary<string, IEnumerable<string>> Claims { get; }

    public IdentityContext(ClaimsPrincipal principal)
    {
        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        UserId = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
        Role = principal.Claims.SingleOrDefault(x=>x.Type == ClaimTypes.Role)?.Value;
        Claims = principal.Claims.GroupBy(x=>x.Type)
            .ToDictionary(x=>x.Key, x=>x.Select(y=>y.Value.ToString()));
    }
}