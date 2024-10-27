using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
}