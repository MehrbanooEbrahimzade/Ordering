using EventBus.Messages.Events;
using MassTransit;
using Order.Api.Controllers;

namespace Order.Api.Services
{
    public class OrderService: IOrderService
    {
        private readonly IRequestClient<OrderSubmittedEvent> _client;
        private readonly ILogger<OrderController> _logger;

        public OrderService(IRequestClient<OrderSubmittedEvent> client, ILogger<OrderController> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<bool> Post(OrderCommand command)
        {
            var response = await _client.GetResponse<OrderSubmittedResponse>(new
            {
                command.Amount,
                command.ProductId,
                command.UserId,
                command.UserName,
                command.Number,
                command.EmailAddress

            });

            _logger.LogInformation("{name} order information published !", command.UserName);

            return response.Message.IsSuccess;
        }
    }
}
