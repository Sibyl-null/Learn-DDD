using DomeGym.Domain.UnitTest.TestUtils.Rooms;
using DomeGym.Domain.UnitTest.TestUtils.Sessions;
using ErrorOr;
using FluentAssertions;

namespace DomeGym.Domain.UnitTest;

public class RoomTests
{
    [Fact]
    public void ScheduleSession_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var room = RoomFactory.CreateRoom(maxSessionCount: 1);
        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid());
        
        // Act
        ErrorOr<Success> scheduleResult1 = room.ScheduleSession(session1);
        ErrorOr<Success> scheduleResult2 = room.ScheduleSession(session2);

        // Assert
        scheduleResult1.IsError.Should().BeFalse();
        scheduleResult2.IsError.Should().BeTrue();
        scheduleResult2.FirstError.Should().Be(RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows);
    }
}