using EventBus.Messages.Events;
using MassTransit;

namespace Email.Api.Consumer
{
    public class OrderSavedConsumer : IConsumer<OrderSavedEvent>
    {
        private readonly ILogger<OrderSavedConsumer> _logger;

        public OrderSavedConsumer(ILogger<OrderSavedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderSavedEvent> context)
        {
            _logger.LogInformation("hello dear {UserName}, your order saved! ", context.Message.UserName);
        }
    }
}
