using DomeGym.Domain.SubscriptionAggregate;
using DomeGym.Domain.UnitTest.TestConstants;

namespace DomeGym.Domain.UnitTest.TestUtils.Subscription;

public static class SubscriptionFactory
{
    public static SubscriptionAggregate.Subscription CreateSubscription(
        Guid? id = null,
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null)
    {
        return new SubscriptionAggregate.Subscription(
            id: id ?? Constants.Subscription.Id,
            subscriptionType: subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
            adminId: adminId ?? Constants.Admin.Id);
    }
}