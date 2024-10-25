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

    public void ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
            throw new Exception("No more spots available for this session.");

        _participantIds.Add(participant.Id);
    }

    public void CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            throw new Exception("Too close to session to cancel reservation.");
        
        if (!_participantIds.Remove(participant.Id))
            throw new Exception("Participant not found in session.");
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minHours = 24;
        return (_date.ToDateTime(_startTime) - utcNow).TotalHours < minHours;
    }
}