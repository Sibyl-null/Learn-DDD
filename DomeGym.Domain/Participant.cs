using ErrorOr;

namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();

    public Guid Id { get; }
    
    public Participant(Guid? id, Guid userId)
    {
        Id = id ?? Guid.NewGuid();
        _userId = userId;
    }

    public ErrorOr<Success> AddSessionToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict($"Session {session.Id} already added to schedule");

        ErrorOr<Success> boolResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (boolResult is { IsError: true, FirstError.Type: ErrorType.Conflict })
            return ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions;

        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}