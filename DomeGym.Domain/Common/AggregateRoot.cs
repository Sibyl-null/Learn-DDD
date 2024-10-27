namespace DomeGym.Domain.Common;

public class AggregateRoot : Entity
{
    protected readonly List<IDomainEvent> _domainEvents = new();
    
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();
        return copy;
    }
}