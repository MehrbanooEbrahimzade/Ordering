using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Sms.Api.Consumer
{
    public class OperationFinishedConsumer : IConsumer<OperationFinishedEvent>
    {
        private readonly ILogger<OperationFinishedConsumer> _logger;

        public OperationFinishedConsumer(ILogger<OperationFinishedConsumer> logger)
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
