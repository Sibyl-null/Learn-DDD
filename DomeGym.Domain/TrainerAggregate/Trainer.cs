using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();

    public Trainer(Guid? id, Guid userId) 
        : base(id ?? Guid.NewGuid())
    {
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