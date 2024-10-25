using ErrorOr;

namespace DomeGym.Domain;

public class Gym
{
    private readonly Guid _id;
    private readonly List<Guid> _roomIds = new();
    private readonly int _maxRoomCount;

    public Gym(Guid? id, int maxRoomCount)
    {
        _id = id ?? Guid.NewGuid();
        _maxRoomCount = maxRoomCount;
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