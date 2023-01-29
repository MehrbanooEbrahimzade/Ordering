using AutoMapper;
using Email.Application.Features.Orders.Commands.SendEmail;
using EventBus.Messages.Events;

namespace Email.Api.Mapper
{
    public class OrderingProfile : Profile
	{
		public OrderingProfile()
		{
			CreateMap<SendEmailCommand, OrderSubmitted>().ReverseMap();
		}
	}
}
