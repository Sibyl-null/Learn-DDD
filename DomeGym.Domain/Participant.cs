namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();

    public Guid Id { get; }
    
    public Participant(Guid? id, Guid userId)
    {
        Id = id ?? Guid.NewGuid();
        _userId = userId;
    }
}