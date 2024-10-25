using ErrorOr;

namespace DomeGym.Domain;

public class Subscription
{
    private readonly Guid _id;
    private readonly List<Guid> _gymIds = new();
    private readonly int _maxGymCount;

    public Subscription(Guid? id, int maxGymCount)
    {
        _id = id ?? Guid.NewGuid();
        _maxGymCount = maxGymCount;
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
            return Error.Conflict($"Gym {gym.Id} already added to subscription");
        
        if (_gymIds.Count >= _maxGymCount)
            return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        
        _gymIds.Add(gym.Id);
        return Result.Success;
    }
}