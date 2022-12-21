namespace Domain.Models.Dto_s;

public class EntityDTO
{
    public virtual int Id { get; protected set; }

    protected EntityDTO()
    {
    }

    protected EntityDTO(int id)
    {
        Id = id;
    }

    public override bool Equals(object obj)
    {
        if (obj is not EntityDTO other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Id.Equals(other.Id);
    }

    public static bool operator ==(EntityDTO a, EntityDTO b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityDTO a, EntityDTO b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}