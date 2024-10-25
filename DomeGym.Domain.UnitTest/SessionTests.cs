using DomeGym.Domain.UnitTest.TestConstants;
using DomeGym.Domain.UnitTest.TestUtils.Participants;
using DomeGym.Domain.UnitTest.TestUtils.Sessions;
using ErrorOr;
using FluentAssertions;
using NSubstitute;

namespace DomeGym.Domain.UnitTest;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreSpot_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipantCount: 1);
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        
        // Act
        ErrorOr<Success> result1 = session.ReserveSpot(participant1);
        ErrorOr<Success> result2 =session.ReserveSpot(participant2);
        
        // Assert
        result1.IsError.Should().BeFalse();
        result2.IsError.Should().BeTrue();
        result2.FirstError.Should().Be(SessionErrors.CannotHaveMoreReservationsThanParticipants);
    }

    [Fact]
    public void CancelReservation_WhenTooCloseToSessionStart_ShouldFailCancellation()
    {
        // Arrange
        var session = SessionFactory.CreateSession();
        var participant = ParticipantFactory.CreateParticipant();
        ErrorOr<Success> reserveResult = session.ReserveSpot(participant);

        IDateTimeProvider timeProvider = Substitute.For<IDateTimeProvider>();
        timeProvider.UtcNow.Returns(Constants.Session.Date.ToDateTime(TimeOnly.MinValue));

        // Act
        ErrorOr<Success> cancelResult = session.CancelReservation(participant, timeProvider);

        // Assert
        reserveResult.IsError.Should().BeFalse();
        cancelResult.IsError.Should().BeTrue();
        cancelResult.FirstError.Should().Be(SessionErrors.CannotCancelReservationTooCloseToSession);
    }
}