namespace Confab.Shared.Abstractions.Kernel.Types;

public abstract class TypeId : IEquatable<TypeId>
{
    public Guid Value { get; }

    protected TypeId(Guid value)
        => Value = value;   
    
    public bool IsEmpty() => Value == Guid.Empty;
    
    public bool Equals(TypeId other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TypeId)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    public static implicit operator Guid(TypeId id) 
        => id.Value;

    public static bool operator ==(TypeId a, TypeId b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a is not null && b is not null)
        {
            return a.Value.Equals(b.Value);
        }
        
        return false;
    }

    public static bool operator !=(TypeId a, TypeId b)
    {
        return !(a == b);
    }
    
    public override string ToString() => Value.ToString();
}