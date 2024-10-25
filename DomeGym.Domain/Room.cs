using ErrorOr;

namespace DomeGym.Domain;

public class Room
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxSessionCount;
    
    public Guid Id { get; }

    public Room(Guid? id, int maxSessionCount)
    {
        Id = id ?? Guid.NewGuid();
        _maxSessionCount = maxSessionCount;
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict($"Session {session.Id} already scheduled in this room.");
        
        if (_sessionIds.Count >= _maxSessionCount)
            return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        
        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}