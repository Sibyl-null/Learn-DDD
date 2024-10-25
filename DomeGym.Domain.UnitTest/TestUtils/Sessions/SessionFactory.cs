using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(int maxParticipants)
    {
        return new Session(
            id: null,
            trainerId: Constants.Trainer.Id,
            maxParticipants: maxParticipants);
    }
}