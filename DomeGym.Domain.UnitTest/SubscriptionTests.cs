using DomeGym.Domain.UnitTest.TestUtils.Gyms;
using DomeGym.Domain.UnitTest.TestUtils.Subscription;
using ErrorOr;
using FluentAssertions;

namespace DomeGym.Domain.UnitTest;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var subscription = SubscriptionFactory.CreateSubscription(maxGymCount: 1);
        var gym1 = GymFactory.CreateGym(id: Guid.NewGuid());
        var gym2 = GymFactory.CreateGym(id: Guid.NewGuid());
        
        // Act
        ErrorOr<Success> addResult1 = subscription.AddGym(gym1);
        ErrorOr<Success> addResult2 = subscription.AddGym(gym2);

        // Assert
        addResult1.IsError.Should().BeFalse();
        addResult2.IsError.Should().BeTrue();
        addResult2.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows);
    }
}