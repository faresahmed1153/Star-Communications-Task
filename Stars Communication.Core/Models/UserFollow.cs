using Star_Communications.Core.Models.Identity;

namespace Star_Communications.Core.Models
{
	public class UserFollow
	{


		public string FollowerId { get; set; }
		public ApplicationUser Follower { get; set; }


		public string FollowingId { get; set; }
		public ApplicationUser Following { get; set; }
	}
}
