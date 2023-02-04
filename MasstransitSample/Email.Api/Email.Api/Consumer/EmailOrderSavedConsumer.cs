using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MassTransit;

namespace Email.Api.Consumer
{
    public class EmailOrderSavedConsumer : IConsumer<OrderSavedEvent>
    {
        private readonly ILogger<EmailOrderSavedConsumer> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;


        public EmailOrderSavedConsumer(ILogger<EmailOrderSavedConsumer> logger, ISendEndpointProvider sendEndpointProvider)
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
