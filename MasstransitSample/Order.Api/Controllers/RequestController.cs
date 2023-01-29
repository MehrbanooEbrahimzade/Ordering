using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestController : ControllerBase
{
    private readonly IRequestClient<OrderSubmittedEvent> _client;
    private readonly ILogger<RequestController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public RequestController(IRequestClient<OrderSubmittedEvent> client, ILogger<RequestController> logger, IPublishEndpoint publishEndpoint)
    {
        _client = client;
        _logger = logger;
        _publishEndpoint = publishEndpoint;

    }

    [HttpPost]
    public async Task<ActionResult> Post(OrderCommand command)
    {
        _logger.LogInformation("{name} order information Sent !", command.UserName);

        //await _publishEndpoint.Publish<OrderSubmittedEvent>(new
        //{
        //    command.User,
        //    command.Amount,
        //    command.ProductId
        //});UserInfoCommand -> UserInfo
        var response = await _client.GetResponse<OrderSubmittedResponse>(new
        {
            command.Amount,
            command.ProductId,
            command.UserId,
            command.UserName,
            command.Number,
            command.EmailAddress

        });

        return Ok(response.Message.IsSuccess);
    }
}