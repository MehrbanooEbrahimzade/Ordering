using Accounting.Api.Commands;
using Accounting.Api.Repository.IRepository;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Transports;

namespace Accounting.Api.Consumer
{
    public class AccountingCheckOrderStatusConsumer : IConsumer<OrderSubmittedEvent>
    {
        private readonly IAccountingRepository _accountingRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<AccountingCheckOrderStatusConsumer> _logger;
        private readonly IMapper _mapper;

        public AccountingCheckOrderStatusConsumer(IAccountingRepository accountingRepository, ILogger<AccountingCheckOrderStatusConsumer> logger, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _accountingRepository = accountingRepository;
            _logger = logger;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            _logger.LogInformation("{name}'s data Received", context.Message.UserName);

            var accounting = new AddAccountingCommand
            {
                Amount = context.Message.Amount,
                ProductId = context.Message.ProductId,
                UserId = context.Message.UserId,
                UserName = context.Message.UserName,
                Number = context.Message.Number,
                EmailAddress = context.Message.EmailAddress,
            };

            var succeed = await _accountingRepository.AddAccount(accounting);

            await context.RespondAsync<OrderSubmittedResponse>(new
            {
                IsSuccess = succeed
            });
            await _publishEndpoint.Publish<OrderSavedEvent>(new
            {
                accounting.ProductId,
                accounting.Amount,
                accounting.UserName,
                accounting.Number,
                accounting.EmailAddress
            });
            _logger.LogInformation("Published OrderSavedEvent");

        }

    }
}