using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.ParticipantAggregate;
using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private readonly Guid _trainerId;
    private readonly int _maxParticipantCount;
    private readonly List<Reservation> _reservations = new();
    
    public DateOnly Date { get; }
    public TimeRange Time { get; }

    public Session(Guid trainerId, int maxParticipantCount, DateOnly date, TimeRange time, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _trainerId = trainerId;
        _maxParticipantCount = maxParticipantCount;
        Date = date;
        Time = time;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_reservations.Count >= _maxParticipantCount)
            return SessionErrors.CannotHaveMoreReservationsThanParticipants;

        if (_reservations.Any(r => r.ParticipantId == participant.Id))
            return Error.Conflict(description: "Participant already reserved spot in this session.");

        _reservations.Add(new Reservation(participant.Id));
        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            return SessionErrors.CannotCancelReservationTooCloseToSession;

        Reservation? reservation = _reservations.Find(r => r.ParticipantId == participant.Id);
        if (reservation == null)
            return Error.NotFound(description: "Participant not found in session.");
        
        _reservations.Remove(reservation);
        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minHours = 24;
        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < minHours;
    }
}