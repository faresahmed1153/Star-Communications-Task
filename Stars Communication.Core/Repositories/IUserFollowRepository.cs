﻿using Star_Communications.Core.Dtos;
using Star_Communications.Core.Models;

namespace Star_Communications.Core.Repositories
{
	public interface IUserFollowRepository<T> : IGenericRepository<T> where T : UserFollow
	{
		void Delete(T user);

		Task<int> GetCountOfFollowersAsync(string userId);

		Task<int> GetCountOfFollowingsAsync(string userId);

		Task<UserFollow> GetUserFollowAsync(string followerId, string followingId);

		Task<IReadOnlyList<string>> GetTweetsOfFollowingsAsync(string id, PaginationDto PaginationDto);

		Task<IReadOnlyList<object>> GetTweetsForMostFollowedFiveUsersAsync(PaginationDto paginationDto);
	}
}
