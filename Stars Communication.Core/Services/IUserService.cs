using Star_Communications.Core.Dtos;

namespace Star_Communications.Core.Services
{
	public interface IUserService
	{
		Task<UserProfileDto> GetUserProfile(string userId);


	}
}
