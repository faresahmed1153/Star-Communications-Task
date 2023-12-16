using AutoMapper;
using Star_Communications.Core.Dtos;
using Star_Communications.Core.Models;
using Star_Communications.Core.Models.Identity;

namespace Star_Communications.Service.Helpers
{
	public class MappingProfiles : Profile
	{


		public MappingProfiles()
		{


			CreateMap<RegisterDto, ApplicationUser>();

			CreateMap<TweetDto, Tweet>();


		}
	}
}
