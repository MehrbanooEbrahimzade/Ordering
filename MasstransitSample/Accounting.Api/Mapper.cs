using Accounting.Api.Commands;
using AutoMapper;
using EventBus.Messages.Events;
using Accounting.Api.Entities;

namespace Accounting.Api
{
    public class AccountingProfile : Profile
    {
        public AccountingProfile()
        {
            CreateMap<AccountingModel, AddAccountingCommand>().ReverseMap();
        }
    }
}
