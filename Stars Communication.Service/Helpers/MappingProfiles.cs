using AutoMapper;
using Stars_Communication.Core.Dtos;
using Stars_Communication.Core.Models;
using Stars_Communication.Core.Models.Identity;

namespace Stars_Communication.Service.Helpers
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
