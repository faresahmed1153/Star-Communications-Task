using Stars_Communication.Core.Dtos;

namespace Stars_Communication.Core.Services
{
	public interface IUserFollowService
	{

		Task<int> GetCountOfFollowingsForUser(string userId);

		Task<int> GetCountOfFollowersForUser(string userId);

		Task<string> UnFollowUser(string currentUserId, string followingId);

		Task<string> FollowUser(string currentUserId, string followingId);

		Task<IReadOnlyList<string>> GetAllTweetsOfFollowings(string currentUserId, PaginationDto paginationDto);

		Task<IReadOnlyList<object>> GetTweetsForMostFollowedFiveUsers(PaginationDto paginationDto);

	}
}
