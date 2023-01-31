using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Transports;

namespace Email.Api.Consumer
{
    public class OrderSavedConsumer : IConsumer<OrderSavedEvent>
    {
        private readonly ILogger<OrderSavedConsumer> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;


        public OrderSavedConsumer(ILogger<OrderSavedConsumer> logger, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<OrderSavedEvent> context)
        {
            _logger.LogInformation("hello dear {UserName}, your order saved! ", context.Message.UserName);

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:operationfinished-queue"));
            await endpoint.Send(new OperationFinishedEvent { UserName = context.Message.UserName, Number = context.Message.Number });
            _logger.LogInformation("Send to Sms service");

        }
    }
}
