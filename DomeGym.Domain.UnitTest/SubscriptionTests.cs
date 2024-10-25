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
        Subscription subscription = SubscriptionFactory.CreateSubscription();
        List<Gym> gyms = Enumerable.Range(0, subscription.GetMaxGymCount() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid()))
            .ToList();

        // Act
        List<ErrorOr<Success>> results = gyms.ConvertAll(subscription.AddGym);

        // Assert
        results.Take(..^1)
            .Should()
            .AllSatisfy(result => result.IsError.Should().BeFalse());

        ErrorOr<Success> lastResult = results.Last();
        lastResult.IsError.Should().BeTrue();
        lastResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows);
    }
}