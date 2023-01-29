using AutoMapper;
using Email.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Email.Application.Features.Orders.Commands.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SendEmailCommandHandler> _logger;
        private readonly IEmailRepository _emailRepository;
        
        public SendEmailCommandHandler(IEmailRepository emailRepository, IMapper mapper, ILogger<SendEmailCommandHandler> logger)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var email = _mapper.Map<Domain.Entities.Email>(request);
            var result = true; //await _emailRepository.CreateEmail(email);
            if (result)
            {
                _logger.LogInformation($"Email {email.UserName} is successfully created.");
                 await SendMail(email);
            }

            return result;
        }

        private async Task SendMail(Domain.Entities.Email email)
        {
            //await SendEmail(email);
             _logger.LogInformation($"Email {email.EmailAddress} is successfully sent.");
             await Task.Delay(2000);
        }
    }
}
