using DomeGym.Domain.Common;

namespace DomeGym.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    private readonly Guid _userId;
    private readonly Guid _subscriptionId;

    protected Admin(Guid? id, Guid userId, Guid subscriptionId) 
        : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
        _subscriptionId = subscriptionId;
    }
}