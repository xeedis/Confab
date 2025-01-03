namespace Confab.Shared.Abstractions.Kernel.Types;

public class ConferenceId : TypeId
{
    public ConferenceId(Guid value) : base(value)
    {
    }
    
    public static implicit operator ConferenceId(Guid value) => new (value);
}