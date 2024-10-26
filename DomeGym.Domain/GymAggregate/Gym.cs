using DomeGym.Domain.Common;
using DomeGym.Domain.RoomAggregate;
using ErrorOr;

namespace DomeGym.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private readonly Guid _subscriptionId;
    private readonly int _maxRoomCount;
    private readonly List<Guid> _roomIds = new();

    public Gym(Guid? id, int maxRoomCount, Guid subscriptionId)
        : base(id ?? Guid.NewGuid())
    {
        _maxRoomCount = maxRoomCount;
        _subscriptionId = subscriptionId;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Contains(room.Id))
            return Error.Conflict($"Room {room.Id} already exists in the gym.");

        if (_roomIds.Count >= _maxRoomCount)
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
        
        _roomIds.Add(room.Id);
        return Result.Success;
    }
}