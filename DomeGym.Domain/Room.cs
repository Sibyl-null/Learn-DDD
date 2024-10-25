namespace DomeGym.Domain;

public class Room
{
    private readonly List<Guid> _sessionIds;
    
    public Guid Id { get; }

    public Room(Guid id)
    {
        Id = id;
    }
}