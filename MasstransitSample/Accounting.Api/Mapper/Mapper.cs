using Accounting.Api.Commands;
using AutoMapper;
using EventBus.Messages.Events;
using Accounting.Api.Entities;

namespace Accounting.Api.Mapper
{
    public class AccountingProfile : Profile
    {
        public AccountingProfile()
        {
            CreateMap<AccountingModel, AddAccountingCommand>().ReverseMap();
        }
    }
}
