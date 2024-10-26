using ErrorOr;

namespace DomeGym.Domain;

public class Trainer
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();

    public Trainer(Guid? id, Guid userId)
    {
        _id = id ?? Guid.NewGuid();
        _userId = userId;
    }

    public ErrorOr<Success> AddSessionToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict($"Session {session.Id} already added to schedule");

        ErrorOr<Success> boolResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (boolResult.IsError)
        {
            return boolResult.FirstError.Type == ErrorType.Conflict
                ? TrainerErrors.CannotHaveTwoOrMoreOverlappingSessions
                : boolResult.Errors;
        }

        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}