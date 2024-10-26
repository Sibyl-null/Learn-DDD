using ErrorOr;

namespace DomeGym.Domain;

public class Subscription
{
    private readonly Guid _id;
    private readonly Guid _adminId;
    private readonly SubscriptionType _subscriptionType;
    private readonly List<Guid> _gymIds = new();

    public Subscription(Guid? id, SubscriptionType subscriptionType, Guid adminId)
    {
        _id = id ?? Guid.NewGuid();
        _subscriptionType = subscriptionType;
        _adminId = adminId;
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

    public int GetMaxRoomCount()
    {
        return _subscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 3,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException("Invalid subscription type")
        };
    }

    public int GetMaxDailySessionCount()
    {
        return _subscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 4,
            nameof(SubscriptionType.Starter) => int.MaxValue,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException("Invalid subscription type")
        };
    }
}