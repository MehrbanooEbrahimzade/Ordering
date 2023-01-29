using MediatR;

namespace Email.Application.Features.Orders.Queries.GetEmail
{
    public class GetEmailQuery : IRequest<GetEmailDto>
    {
        public Guid OrderId { get; set; }

        public GetEmailQuery(Guid orderId)
        {
            OrderId = orderId ;
        }
    }
}
