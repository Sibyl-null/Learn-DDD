using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new();
    private readonly int _maxParticipants;
    private readonly DateOnly _date;
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;

    public Session(Guid? id, Guid trainerId, int maxParticipants, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        _id = id ?? Guid.NewGuid();
        _trainerId = trainerId;
        _maxParticipants = maxParticipants;
        _date = date;
        _startTime = startTime;
        _endTime = endTime;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
            return SessionErrors.CannotHaveMoreReservationsThanParticipants;

        _participantIds.Add(participant.Id);
        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            return SessionErrors.CannotCancelReservationTooCloseToSession;

        if (!_participantIds.Remove(participant.Id))
            return Error.NotFound("Participant not found in session.");
        
        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minHours = 24;
        return (_date.ToDateTime(_startTime) - utcNow).TotalHours < minHours;
    }
}