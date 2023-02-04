using EventBus.Messages.Events;
using MassTransit;

namespace Sms.Api.Consumer
{
    public class SmsOperationFinishedConsumer : IConsumer<OperationFinishedEvent>
    {
        private readonly ILogger<SmsOperationFinishedConsumer> _logger;

        public SmsOperationFinishedConsumer(ILogger<SmsOperationFinishedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OperationFinishedEvent> context)
        {
            _logger.LogInformation("dear customer with number :{PhoneNumber}, your order status is --DONE--", context.Message.Number);
            _logger.LogInformation("-------------------");
            return Task.CompletedTask;
        }
    }

}
