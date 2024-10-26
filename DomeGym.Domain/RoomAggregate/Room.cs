using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private readonly Guid _gymId;
    private readonly int _maxDailySessionCount;
    private readonly List<Guid> _sessionIds = new();
    private readonly Schedule _schedule = Schedule.Empty();
    
    public Room(int maxDailySessionCount, Guid gymId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _maxDailySessionCount = maxDailySessionCount;
        _gymId = gymId;
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict(description: $"Session {session.Id} already scheduled in this room.");
        
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