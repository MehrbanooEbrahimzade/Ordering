using AutoMapper;
using Email.Application.Contracts.Persistence;
using MediatR;

namespace Email.Application.Features.Orders.Queries.GetEmail
{
    public class GetEmailQueryHandler : IRequestHandler<GetEmailQuery, GetEmailDto>
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IMapper _mapper;

        public GetEmailQueryHandler(IEmailRepository emailRepository, IMapper mapper)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
        }

        public async Task<GetEmailDto> Handle(GetEmailQuery request, CancellationToken cancellationToken)
        {
            var email = await _emailRepository.GetEmailByOrderId(request.OrderId);
            return _mapper.Map<GetEmailDto>(email);
        }
    }
}
