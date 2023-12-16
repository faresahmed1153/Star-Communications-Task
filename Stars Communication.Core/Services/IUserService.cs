using Stars_Communication.Core.Dtos;

namespace Stars_Communication.Core.Services
{
	public interface IUserService
	{
		Task<UserProfileDto> GetUserProfile(string userId);


	}
}
