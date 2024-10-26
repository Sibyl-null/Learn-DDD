using ErrorOr;

namespace DomeGym.Domain;

public class Room
{
    private readonly Guid _gymId;
    private readonly int _maxDailySessionCount;
    private readonly List<Guid> _sessionIds = new();
    private readonly Schedule _schedule = Schedule.Empty();
    
    public Guid Id { get; }

    public Room(Guid? id, int maxDailySessionCount, Guid gymId)
    {
        Id = id ?? Guid.NewGuid();
        _maxDailySessionCount = maxDailySessionCount;
        _gymId = gymId;
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict($"Session {session.Id} already scheduled in this room.");
        
        if (_sessionIds.Count >= _maxDailySessionCount)
            return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;

        ErrorOr<Success> boolResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (boolResult.IsError)
        {
            return boolResult.FirstError.Type == ErrorType.Conflict
                ? RoomErrors.CannotHaveTwoOrMoreOverlappingSessions
                : boolResult.Errors;
        }

        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}