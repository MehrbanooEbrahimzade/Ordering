using MediatR;

namespace Email.Application.Features.Orders.Commands.DeleteEmail
{
    public class DeleteEmailCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
