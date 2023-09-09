using AuthServer.Models;
using AuthServer.Pages.Account.Register;
using AutoMapper;

namespace AuthServer.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<InputModel, ApplicationUser>();
		}
	}
}
