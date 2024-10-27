using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.SubscriptionAggregate;
using MediatR;
using ErrorOr;

namespace DomeGym.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly IAdminsRepository _adminsRepository;

    public CreateSubscriptionCommandHandler(IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        Admin? admin = await _adminsRepository.GetByIdAsync(request.AdminId);
        if (admin == null)
            return Error.NotFound(description: "Admin not found");

        if (admin.SubscriptionId is not null)
            return Error.Conflict(description: "Admin already has a subscription");

        Subscription subscription = new Subscription(request.SubscriptionType, request.AdminId);
        ErrorOr<Success> result = admin.SetSubscription(subscription);
        if (result.IsError)
            return result.Errors;

        await _adminsRepository.UpdateAsync(admin);
        return subscription;
    }
}