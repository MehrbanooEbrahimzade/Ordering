using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestController : ControllerBase
{
    private readonly IRequestClient<OrderSubmitted> _client;
    private readonly ILogger<RequestController> _logger;
    public RequestController(IRequestClient<OrderSubmitted> client, ILogger<RequestController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> Post(OrderCommand command)
    {
        _logger.LogInformation("{name} order Information Sent !", command.UserName);

        var response = await _client.GetResponse<OrderSubmittedResponse>(new
        {
            command.UserId,
            command.UserName,
            command.ProductId,
            command.Amount

        });

        return Ok(response.Message.IsSuccess);
    }
}