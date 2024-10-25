using ErrorOr;

namespace DomeGym.Domain;

public class Room
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxSessionCount;
    private readonly Schedule _schedule;
    
    public Guid Id { get; }

    public Room(Guid? id, int maxSessionCount, Schedule? schedule = null)
    {
        Id = id ?? Guid.NewGuid();
        _maxSessionCount = maxSessionCount;
        _schedule = schedule ?? Schedule.Empty();
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict($"Session {session.Id} already scheduled in this room.");
        
        if (_sessionIds.Count >= _maxSessionCount)
            return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;

        ErrorOr<Success> boolResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (boolResult is { IsError: true, FirstError.Type: ErrorType.Conflict })
            return RoomErrors.CannotHaveTwoOrMoreOverlappingSessions;

        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}