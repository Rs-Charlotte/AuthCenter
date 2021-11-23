using DotNetCore.CAP;
using MediatR;

namespace AuthCenter.Admin.Application.IntegrationEvents
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        IMediator _mediator;

        public SubscriberService(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
