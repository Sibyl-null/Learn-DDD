using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Subscription;

public static class SubscriptionFactory
{
    public static Domain.Subscription CreateSubscription(
        Guid? id = null,
        SubscriptionType? subscriptionType = null)
    {
        return new Domain.Subscription(
            id: id ?? Constants.Subscription.Id,
            subscriptionType: subscriptionType ?? Constants.Subscription.DefaultSubscriptionType);
    }
}