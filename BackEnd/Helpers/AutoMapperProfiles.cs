using AutoMapper;
using BackEnd.DTOs;
using BackEnd.Entities;
using BackEnd.Extensions;

namespace BackEnd.Helpers

;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        _ = CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        _ = CreateMap<Photo, PhotoDto>();
        _ = CreateMap<MemberUpdateDto, AppUser>();
        _ = CreateMap<RegisterDto, AppUser>();
    }
}