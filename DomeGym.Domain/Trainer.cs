using ErrorOr;

namespace DomeGym.Domain;

public class Trainer
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly Schedule _schedule;
    private readonly List<Guid> _sessionIds = new();

    public Trainer(Guid? id, Schedule? schedule = null)
    {
        _schedule = schedule ?? Schedule.Empty();
        _id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> AddSessionToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict($"Session {session.Id} already added to schedule");

        ErrorOr<Success> boolResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (boolResult is { IsError: true, FirstError.Type: ErrorType.Conflict })
            return TrainerErrors.CannotHaveTwoOrMoreOverlappingSessions;

        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}