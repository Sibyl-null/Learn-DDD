using DomeGym.Domain.Common;
using DomeGym.Domain.SubscriptionAggregate;
using ErrorOr;

namespace DomeGym.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; }

    protected Admin(Guid userId, Guid subscriptionId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue)
            return Error.Conflict(description: "Subscription already set for this admin");
        
        SubscriptionId = subscription.Id;
        return Result.Success;
    }
}