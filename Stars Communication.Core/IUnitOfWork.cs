using Star_Communications.Core.Models;
using Star_Communications.Core.Repositories;

namespace Star_Communications.Core
{
	public interface IUnitOfWork : IAsyncDisposable
	{

		public IGenericRepository<Tweet> TweetRepo { get; set; }

		public IUserRepository UserRepo { get; set; }

		public IUserFollowRepository<UserFollow> UserFollowRepo { get; set; }


		Task<int> Complete();
	}
}
