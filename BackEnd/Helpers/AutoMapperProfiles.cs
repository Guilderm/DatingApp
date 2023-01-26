using API.DTOs;
using API.Entities;
using API.Extensions;

using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		_ = CreateMap<AppUser, MemberDto>()
			.ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
			.ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
		_ = CreateMap<Photo, PhotoDto>();
		_ = CreateMap<MemberUpdateDto, AppUser>();
		_ = CreateMap<RegisterDto, AppUser>();
		_ = CreateMap<Message, MessageDto>()
			.ForMember(d => d.SenderPhotoUrl, o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
			.ForMember(d => d.RecipientPhotoUrl, o => o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
	}
}