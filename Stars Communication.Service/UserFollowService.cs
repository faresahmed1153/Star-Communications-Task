using Star_Communications.Core;
using Star_Communications.Core.Dtos;
using Star_Communications.Core.Models;
using Star_Communications.Core.Services;

namespace Star_Communications.Service
{
	public class UserFollowService : IUserFollowService
	{
		private readonly IUnitOfWork _unitOfWork;

		public UserFollowService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		public async Task<int> GetCountOfFollowersForUser(string userId)

			=> await _unitOfWork.UserFollowRepo.GetCountOfFollowersAsync(userId);


		public async Task<int> GetCountOfFollowingsForUser(string userId)

		=> await _unitOfWork.UserFollowRepo.GetCountOfFollowingsAsync(userId);

		public async Task<string> UnFollowUser(string currentUserId, string followingId)
		{
			if (currentUserId == followingId)
				return "Invalid Id you can't unfollow yourself";

			var userFollow = await _unitOfWork.UserFollowRepo.GetUserFollowAsync(currentUserId, followingId);

			if (userFollow is null)
				return "Invalid Id";

			_unitOfWork.UserFollowRepo.Delete(userFollow);

			var affectedRows = await _unitOfWork.Complete();

			if (affectedRows > 0)
				return "";


			return "hmmm looks like unfollowing user failed!";

		}


		public async Task<string> FollowUser(string currentUserId, string followingId)
		{
			if (currentUserId == followingId)
				return "Invalid Id you can't follow yourself";

			var userToBeFollowed = await _unitOfWork.UserRepo.GetByIdAsync(followingId);

			if (userToBeFollowed is null)
				return "Invalid Id";

			var userFollow = await _unitOfWork.UserFollowRepo.GetUserFollowAsync(currentUserId, followingId);

			if (userFollow is not null)
				return "You already have followed this user once before";



			await _unitOfWork.UserFollowRepo.AddAsync(new UserFollow
			{
				FollowerId = currentUserId,
				FollowingId = followingId
			});

			var affectedRows = await _unitOfWork.Complete();

			if (affectedRows > 0)
				return "";


			return "hmmm looks like following user failed!";

		}

		public async Task<IReadOnlyList<string>> GetAllTweetsOfFollowings(string currentUserId, PaginationDto paginationDto)
		{

			var tweets = await _unitOfWork.UserFollowRepo.GetTweetsOfFollowingsAsync(currentUserId, paginationDto);

			var shuffledTweets = tweets.OrderBy(t => Random.Shared.Next()).ToList();

			return shuffledTweets;
		}

		public async Task<IReadOnlyList<object>> GetTweetsForMostFollowedFiveUsers(PaginationDto paginationDto)
		{

			var tweets = await _unitOfWork.UserFollowRepo.GetTweetsForMostFollowedFiveUsersAsync(paginationDto);

			return tweets;
		}
	}
}
