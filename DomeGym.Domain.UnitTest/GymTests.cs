using DomeGym.Domain.UnitTest.TestUtils.Gyms;
using DomeGym.Domain.UnitTest.TestUtils.Rooms;
using ErrorOr;
using FluentAssertions;

namespace DomeGym.Domain.UnitTest;

public class GymTests
{
    [Fact]
    public void AddRoom_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var gym = GymFactory.CreateGym(maxRoomCount: 1);
        var room1 = RoomFactory.CreateRoom(id: Guid.NewGuid());
        var room2 = RoomFactory.CreateRoom(id: Guid.NewGuid());
        
        // Act
        ErrorOr<Success> addResult1 = gym.AddRoom(room1);
        ErrorOr<Success> addResult2 = gym.AddRoom(room2);

        // Assert
        addResult1.IsError.Should().BeFalse();
        addResult2.IsError.Should().BeTrue();
        addResult2.FirstError.Should().Be(GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows);
    }
}