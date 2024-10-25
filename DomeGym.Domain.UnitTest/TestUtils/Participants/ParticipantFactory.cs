using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Participants;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(Guid? id, Guid? userId)
    {
        return new Participant(
            id: id ?? Constants.Participant.Id,
            userId: userId ?? Constants.User.Id);
    }
}