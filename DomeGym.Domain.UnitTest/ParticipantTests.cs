using DomeGym.Domain.UnitTest.TestUtils.Common;
using DomeGym.Domain.UnitTest.TestUtils.Participants;
using DomeGym.Domain.UnitTest.TestUtils.Sessions;
using ErrorOr;
using FluentAssertions;

namespace DomeGym.Domain.UnitTest;

public class ParticipantTests
{
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        var participant = ParticipantFactory.CreateParticipant();
        
        var session1 = SessionFactory.CreateSession(
            id: Guid.NewGuid(),
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1));
        
        var session2 = SessionFactory.CreateSession(
            id: Guid.NewGuid(),
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2));
        
        // Act
        ErrorOr<Success> result1 = participant.AddSessionToSchedule(session1);
        ErrorOr<Success> result2 = participant.AddSessionToSchedule(session2);

        // Assert
        result1.IsError.Should().BeFalse();
        result2.IsError.Should().BeTrue();
        result2.FirstError.Should().Be(ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}