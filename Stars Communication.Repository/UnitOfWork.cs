using Stars_Communication.Core;
using Stars_Communication.Core.Models;
using Stars_Communication.Core.Repositories;
using Stars_Communication.Repository.Repositories;


namespace Stars_Communication.Repository
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
