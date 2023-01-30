using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestController : ControllerBase
{
    private readonly IRequestClient<OrderSubmittedEvent> _client;
    private readonly ILogger<RequestController> _logger;

    public RequestController(IRequestClient<OrderSubmittedEvent> client, ILogger<RequestController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> Post(OrderCommand command)
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

        return Ok(response.Message.IsSuccess);
    }
}