using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MassTransit;

namespace Email.Api.Consumer
{
    public class OrderSubmittedConsumer : IConsumer<OrderSubmittedEvent>
    {
        private readonly ILogger<OrderSubmittedConsumer> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public OrderSubmittedConsumer(ILogger<OrderSubmittedConsumer> logger, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            _logger.LogInformation("hello dear { UserName}, We have received your order and we are currently preparing your order!", context.Message.UserName);
            
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(EventBusConstants.OperationFinishedQueue));

            await endpoint.Send(new OperationFinishedEvent { UserName = context.Message.UserName, Number = context.Message.Number });
        }

    }

}
