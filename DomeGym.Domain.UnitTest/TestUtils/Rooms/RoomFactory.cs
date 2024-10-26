using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        Guid? id = null,
        int maxSessionCount = Constants.Room.MaxSessionCount,
        Guid? gymId = null)
    {
        return new Room(
            id: id ?? Constants.Room.Id,
            maxDailySessionCount: maxSessionCount,
            gymId: gymId ?? Constants.Gym.Id);
    }
}