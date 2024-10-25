using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(Guid? id = null, int maxSessionCount = Constants.Room.MaxSessionCount)
    {
        return new Room(
            id: id ?? Constants.Room.Id,
            maxSessionCount);
    }
}