using Microsoft.AspNetCore.Identity;


namespace Stars_Communication.Core.Models.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }

		public ICollection<Tweet> Tweets { get; set; } = new HashSet<Tweet>();

		public ICollection<UserFollow> Followings { get; set; } = new HashSet<UserFollow>();

		public ICollection<UserFollow> Followers { get; set; } = new HashSet<UserFollow>();


	}
}
