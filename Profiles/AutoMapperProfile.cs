using AutoMapper;
using M = ThePortfo.Models;
using ThePortfo.Models.DTOs;
namespace ThePortfo.Profiles;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<ProfileDTO, M.Profile>();
		CreateMap<ThePortfo.Models.Profile, ProfileDTO>();
		CreateMap<TemplateDTO, M.Template>();
		CreateMap<M.Template, TemplateDTO>();
	}
}
