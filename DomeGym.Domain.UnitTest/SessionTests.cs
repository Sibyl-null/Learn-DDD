using DomeGym.Domain.UnitTest.TestUtils.Participants;
using DomeGym.Domain.UnitTest.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.UnitTest;

public class SessionTests
{
    [Fact(DisplayName = "超过参与者最大数量时，预约失败")]
    public void ReserveSpot_WhenNoMoreSpot_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        
        // Act
        session.ReserveSpot(participant1);
        Action action = () => session.ReserveSpot(participant2);
        
        // Assert
        action.Should().Throw<Exception>();
    }
}