using DomeGym.Api.Controllers.Common;
using DomeGym.Application.Subscriptions.Commands.CreateSubscription;
using DomeGym.Contracts.Subscriptions;
using DomeGym.Domain.SubscriptionAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace DomeGym.Api.Controllers;

[Route("subscriptions")]
public class SubscriptionController : ApiController
{
    private readonly ISender _sender;

    public SubscriptionController(ISender sender)
    {
        _sender = sender;
    }

    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        if (!Domain.SubscriptionAggregate.SubscriptionType.TryFromName(
                request.SubscriptionType.ToString(),
                out var subscriptionType))
        {
            return Problem("Invalid subscription type", statusCode: StatusCodes.Status400BadRequest);
        }

        var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId);
        ErrorOr<Subscription> result = await _sender.Send(command);

        return result.Match(
            subscription => Ok(new SubscriptionResponse(subscription.Id, ToDto(subscription.SubscriptionType))),
            Problem);
    }
    
    private static Contracts.Subscriptions.SubscriptionType ToDto(Domain.SubscriptionAggregate.SubscriptionType subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(Domain.SubscriptionAggregate.SubscriptionType.Free) => Contracts.Subscriptions.SubscriptionType.Free,
            nameof(Domain.SubscriptionAggregate.SubscriptionType.Starter) => Contracts.Subscriptions.SubscriptionType.Starter,
            nameof(Domain.SubscriptionAggregate.SubscriptionType.Pro) => Contracts.Subscriptions.SubscriptionType.Pro,
            _ => throw new InvalidOperationException(),
        };
    }
}