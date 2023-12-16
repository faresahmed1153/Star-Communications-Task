using Stars_Communication.Core.Dtos;

namespace Stars_Communication.Core.Services
{
	public interface ITweetService
	{
		Task<string> CreateTweet(string userId, TweetDto tweetDto);
	}
}
