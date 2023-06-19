using API_Exercise.Domain.Models;
using AutoMapper;

namespace API_Exercise.API.DTOs
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.TimeRegistration, opt => opt.MapFrom(src => src.TimeRegistration));

            CreateMap<TimeRegistration, TimeRegistrationDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));

            CreateMap<Company, CompanyDTO>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));

            CreateMap<TimeRegistrationPostDTO, TimeRegistration>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End));

            CreateMap<TimeRegistrationPutDTO, TimeRegistration>();
        }
    }
}
