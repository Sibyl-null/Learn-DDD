namespace DomeGym.Domain.Common;

public class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
}