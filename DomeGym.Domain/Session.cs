using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new();
    private readonly int _maxParticipantCount;
    
    public Guid Id { get; }
    public DateOnly Date { get; }
    public TimeRange Time { get; }

    public Session(Guid? id, Guid trainerId, int maxParticipantCount, DateOnly date, TimeRange time)
    {
        Id = id ?? Guid.NewGuid();
        _trainerId = trainerId;
        _maxParticipantCount = maxParticipantCount;
        Date = date;
        Time = time;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipantCount)
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
        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < minHours;
    }
}