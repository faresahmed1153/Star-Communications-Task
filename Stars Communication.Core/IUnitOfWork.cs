using Stars_Communication.Core.Models;
using Stars_Communication.Core.Repositories;

namespace Stars_Communication.Core
{
	public interface IUnitOfWork : IAsyncDisposable
	{

		public IGenericRepository<Tweet> TweetRepo { get; set; }

		public IUserRepository UserRepo { get; set; }

		public IUserFollowRepository<UserFollow> UserFollowRepo { get; set; }


		Task<int> Complete();
	}
}
