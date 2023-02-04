using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Sms.Api.Consumer
{
    public class SmsOrderSavedConsumer : IConsumer<OrderSavedEvent>
    {
        private readonly ILogger<SmsOrderSavedConsumer> _logger;

        public SmsOrderSavedConsumer(ILogger<SmsOrderSavedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderSavedEvent> context)
        {
            _logger.LogInformation("hello dear {UserName}, your order saved! ", context.Message.UserName);
            return Task.CompletedTask;
        }
    }
}
