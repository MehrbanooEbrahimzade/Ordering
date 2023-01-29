using AutoMapper;
using Email.Application.Features.Orders.Commands.SendEmail;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace Email.Api.EventBusConsumer
{
    public class SubmitOrderConsumer : IConsumer<OrderSubmitted>
    {
        private readonly ILogger<SubmitOrderConsumer> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            var command = _mapper.Map<SendEmailCommand>(context.Message);
            var result = await _mediator.Send(command);
            _logger.LogInformation(result ? "Email sent to {UserName}" : "The email wasn't sent to {UserName}", context.Message.UserName);
        }
    }

}
