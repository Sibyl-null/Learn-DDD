namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new();
    private readonly int _maxParticipants;

    public Session(Guid? id, Guid trainerId, int maxParticipants)
    {
        _id = id ?? Guid.NewGuid();
        _trainerId = trainerId;
        _maxParticipants = maxParticipants;
    }

    public void ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
            throw new Exception("No more spots available for this session.");

        _participantIds.Add(participant.Id);
    }
}