using Accounting.Api.Commands;
using Accounting.Api.Repository.IRepository;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;

namespace Accounting.Api.RequestConsumer
{
    public class CheckOrderStatusConsumer : IConsumer<OrderSubmittedEvent>
    {
        private readonly IAccountingRepository _accountingRepository;
        private readonly ILogger<CheckOrderStatusConsumer> _logger;
        private readonly IMapper _mapper;

        public CheckOrderStatusConsumer(IAccountingRepository accountingRepository, ILogger<CheckOrderStatusConsumer> logger, IMapper mapper)
        {
            _accountingRepository = accountingRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            _logger.LogInformation("{name} Receive data ", context.Message.UserName);

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
        }
    }
}