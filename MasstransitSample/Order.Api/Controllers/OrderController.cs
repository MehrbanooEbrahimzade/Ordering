using Microsoft.AspNetCore.Mvc;
using Order.Api.Services;

namespace Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult> Post(OrderCommand command)
    {
        var isSuccess = await _orderService.Post(command);
        return Ok(isSuccess);
    }
}