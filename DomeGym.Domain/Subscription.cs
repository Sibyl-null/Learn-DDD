using ErrorOr;

namespace DomeGym.Domain;

public class Subscription
{
    private readonly Guid _id;
    private readonly SubscriptionType _subscriptionType;
    private readonly List<Guid> _gymIds = new();

    public Subscription(Guid? id, SubscriptionType subscriptionType)
    {
        _id = id ?? Guid.NewGuid();
        _subscriptionType = subscriptionType;
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
            return Error.Conflict($"Gym {gym.Id} already added to subscription");
        
        if (_gymIds.Count >= GetMaxGymCount())
            return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        
        _gymIds.Add(gym.Id);
        return Result.Success;
    }

    public int GetMaxGymCount()
    {
        return _subscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new InvalidOperationException("Invalid subscription type")
        };
    }
}