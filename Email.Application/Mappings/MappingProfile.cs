using AutoMapper;
using Email.Application.Features.Orders.Commands.SendEmail;
using Email.Application.Features.Orders.Queries.GetEmail;

namespace Email.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Email, GetEmailDto>().ReverseMap();
            CreateMap<Domain.Entities.Email, SendEmailCommand>().ReverseMap();
        }
    }
}
