using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Net;
using Email.Application.Features.Orders.Commands.DeleteEmail;
using Email.Application.Features.Orders.Commands.SendEmail;
using Email.Application.Features.Orders.Queries.GetEmail;

namespace Email.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // testing purpose
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] SendEmailCommand command)
        {
           var result = await _mediator.Send(command);
           return result? Ok(): BadRequest();
            
        }

        // Delete Email by guid id
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteEmail(Guid id)
        {
            await _mediator.Send(new DeleteEmailCommand { Id = id });
            return Ok();
        }

        // Get Email by guid orderId
        [HttpGet("{orderId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetEmailDto>> GetEmailByOrderId(Guid orderId)
        {
            var email = await _mediator.Send(new GetEmailQuery(orderId));
            return Ok(email);
        }
    }
}