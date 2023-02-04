using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MassTransit;

namespace Email.Api.Consumer
{
    public class EmailOrderSubmittedConsumer : IConsumer<OrderSubmittedEvent>
    {
        private readonly ILogger<EmailOrderSubmittedConsumer> _logger;

        public EmailOrderSubmittedConsumer(ILogger<EmailOrderSubmittedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            _logger.LogInformation("hello dear { UserName}, We have received your order and we are currently preparing your order!", context.Message.UserName);
            return Task.CompletedTask;
        }

    }

}
