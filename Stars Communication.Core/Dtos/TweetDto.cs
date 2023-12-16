using System.ComponentModel.DataAnnotations;

namespace Stars_Communication.Core.Dtos
{
	public class TweetDto
	{
		[Required]
		[RegularExpression(@"^.{1,300}$", ErrorMessage = "Tweet Has a minimum length of 1 character and has a maximum length of 300 characters")]
		public string Content { get; set; }

		public string? UserId { get; set; }

	}
}
