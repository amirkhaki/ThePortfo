using AutoMapper;
using ThePortfo.Models.DTOs;
namespace ThePortfo.Profiles;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<ProfileDTO, ThePortfo.Models.Profile>();
		CreateMap<ThePortfo.Models.Profile, ProfileDTO>();
	}
}
