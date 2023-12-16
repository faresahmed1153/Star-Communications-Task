using Microsoft.EntityFrameworkCore;
using Star_Communications.Core.Dtos;
using Star_Communications.Core.Models;
using Star_Communications.Core.Repositories;

namespace Star_Communications.Repository.Repositories
{
	public class UserFollowRepository<T> : IUserFollowRepository<T> where T : UserFollow
	{
		private readonly AppDbContext _dbContext;


		public UserFollowRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}


		public async Task<UserFollow> GetUserFollowAsync(string followerId, string followingId)

			=> await _dbContext.Set<T>().FirstOrDefaultAsync(uf => uf.FollowerId == followerId && uf.FollowingId == followingId);



		public async Task AddAsync(T entity)

			=> await _dbContext.Set<T>().AddAsync(entity);



		public void Delete(T entity)

			=> _dbContext.Set<T>().Remove(entity);



		public async Task<int> GetCountOfFollowersAsync(string userId)

			=> await _dbContext.Set<T>().CountAsync(uf => uf.FollowingId == userId);


		public async Task<int> GetCountOfFollowingsAsync(string userId)

		=> await _dbContext.Set<T>().CountAsync(uf => uf.FollowerId == userId);


		public async Task<IReadOnlyList<string>> GetTweetsOfFollowingsAsync(string id, PaginationDto paginationDto)
			=> await _dbContext.UserFollows
			.Where(uf => uf.FollowerId == id)
			.Include(uf => uf.Following)
			.ThenInclude(u => u.Tweets)
			.SelectMany(uf => uf.Following.Tweets.Select(t => t.Content))
			.Skip((paginationDto.Page - 1) * paginationDto.PageSize)
			.Take(paginationDto.PageSize)
			.ToListAsync();


		public async Task<IReadOnlyList<object>> GetTweetsForMostFollowedFiveUsersAsync(PaginationDto paginationDto)
			=> await _dbContext.UserFollows

			.GroupBy(uf => uf.FollowingId)
			.OrderByDescending(gp => gp.Count())
			.Take(5)
			.Select(gp =>
			new
			{
				FollowingId = gp.Key
			}

			).Join(_dbContext.Users,
				f => f.FollowingId,
				u => u.Id,
				(f, u) => new
				{
					FollowingId = u.Id,
				}
				).Join(_dbContext.Tweets,
				u => u.FollowingId,
				t => t.UserId,
				(u, t) => t.Content

				)

			.Skip((paginationDto.Page - 1) * paginationDto.PageSize)
			.Take(paginationDto.PageSize)
			.ToListAsync();

	}
}
