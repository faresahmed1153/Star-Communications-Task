using Star_Communications.Core.Models.Identity;

namespace Star_Communications.Core.Models
{
	public class Tweet
	{

		public int TweetId { get; set; }

		public string Content { get; set; }

		public DateTime CreatedAt { get; set; }


		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

	}
}
