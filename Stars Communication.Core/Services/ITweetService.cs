using Star_Communications.Core.Dtos;

namespace Star_Communications.Core.Services
{
	public interface ITweetService
	{
		Task<string> CreateTweet(string userId, TweetDto tweetDto);
	}
}
