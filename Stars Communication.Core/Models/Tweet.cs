using Stars_Communication.Core.Models.Identity;

namespace Stars_Communication.Core.Models
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
