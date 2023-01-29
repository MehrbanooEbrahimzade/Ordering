using MediatR;

namespace Email.Application.Features.Orders.Commands.SendEmail
{
    public class SendEmailCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int Amount { get; set; }
    }
}
