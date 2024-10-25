using DomeGym.Domain.UnitTest.TestConstants;
using DomeGym.Domain.UnitTest.TestUtils.Participants;
using DomeGym.Domain.UnitTest.TestUtils.Sessions;
using FluentAssertions;
using NSubstitute;

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

    [Fact(DisplayName = "临近课程开始时，禁止取消预约")]
    public void CancelReservation_WhenTooCloseToSessionStart_ShouldFailCancellation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            startTime: Constants.Session.StartTime,
            endTime: Constants.Session.EndTime);
        
        var participant = ParticipantFactory.CreateParticipant();
        session.ReserveSpot(participant);

        IDateTimeProvider timeProvider = Substitute.For<IDateTimeProvider>();
        timeProvider.UtcNow.Returns(Constants.Session.Date.ToDateTime(TimeOnly.MinValue));

        // Act
        Action action = () => session.CancelReservation(participant, timeProvider);
        
        // Assert
        action.Should().Throw<Exception>();
    }
}