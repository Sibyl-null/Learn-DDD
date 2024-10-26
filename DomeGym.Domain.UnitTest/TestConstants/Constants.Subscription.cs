using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Domain.UnitTest.TestConstants;

public static partial class Constants
{
    public static class Subscription
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
    }
}