using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        Guid? id = null,
        int maxRoomCount = Constants.Gym.MaxRoomCount,
        Guid? subscriptionId = null)
    {
        return new Gym(
            id: id ?? Constants.Gym.Id,
            maxRoomCount: maxRoomCount,
            subscriptionId: subscriptionId ?? Constants.Subscription.Id);
    }
}