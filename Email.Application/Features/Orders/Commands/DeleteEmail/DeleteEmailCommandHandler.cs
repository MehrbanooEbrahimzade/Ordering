using AutoMapper;
using Email.Application.Contracts.Persistence;
using Email.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Email.Application.Features.Orders.Commands.DeleteEmail
{
    public class DeleteEmailCommandHandler : IRequestHandler<DeleteEmailCommand>
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<DeleteEmailCommandHandler> _logger;

        public DeleteEmailCommandHandler(IEmailRepository emailRepository,ILogger<DeleteEmailCommandHandler> logger)
        {
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailRepository.DeleteEmail(request.Id);
            if (!result)
            {
                throw new NotFoundException(nameof(Email), request.Id);
            }

            _logger.LogInformation($"Email {request.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
