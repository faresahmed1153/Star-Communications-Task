using Star_Communications.Core;
using Star_Communications.Core.Models;
using Star_Communications.Core.Repositories;
using Star_Communications.Repository.Repositories;


namespace Star_Communications.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _dbContext;


		public IGenericRepository<Tweet> TweetRepo { get; set; }

		public IUserRepository UserRepo { get; set; }

		public IUserFollowRepository<UserFollow> UserFollowRepo { get; set; }


		public UnitOfWork(AppDbContext dbContext)
		{
			_dbContext = dbContext;

			TweetRepo = new TweetRepository<Tweet>(_dbContext);

			UserRepo = new UserRepository(_dbContext);

			UserFollowRepo = new UserFollowRepository<UserFollow>(_dbContext);

		}



		public async Task<int> Complete()

		=> await _dbContext.SaveChangesAsync();



		public async ValueTask DisposeAsync()

		=> await _dbContext.DisposeAsync();


	}
}
