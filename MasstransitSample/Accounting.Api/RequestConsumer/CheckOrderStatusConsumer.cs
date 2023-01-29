using Accounting.Api.Commands;
using Accounting.Api.Entities;
using Accounting.Api.Repository.IRepository;
using EventBus.Messages.Events;
using MassTransit;
using static MassTransit.ValidationResultExtensions;

namespace Accounting.Api.RequestConsumer
{
    public class CheckOrderStatusConsumer : IConsumer<OrderSubmitted>
    {
        private readonly IAccountingRepository _accountingRepository;
        private readonly ILogger<CheckOrderStatusConsumer> _logger;

        public CheckOrderStatusConsumer(IAccountingRepository accountingRepository, ILogger<CheckOrderStatusConsumer> logger)
        {
            _accountingRepository = accountingRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            _logger.LogInformation("{name} Receive data ", context.Message.UserName);
            
            var accounting = new AddAccountingCommand
            {
                UserId = context.Message.UserId,
                UserName = context.Message.UserName,
                Amount = context.Message.Amount,
                ProductId = context.Message.ProductId
            };

            var succeed = await _accountingRepository.AddAccount(accounting);
           
            await context.RespondAsync<OrderSubmittedResponse>(new
            {
                IsSuccess = succeed
            });
        }
    }
}