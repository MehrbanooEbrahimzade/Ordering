using EventBus.Messages.Events;
using MassTransit;

namespace Email.Api.Consumer
{
    public class OrderSubmittedConsumer : IConsumer<OrderSubmittedEvent>
    {
        private readonly ILogger<OrderSubmittedConsumer> _logger;

        public OrderSubmittedConsumer(ILogger<OrderSubmittedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            _logger.LogInformation("hello dear { UserName}, We have received your order and we are currently preparing your order!", context.Message.UserName);
        }
    }

}
