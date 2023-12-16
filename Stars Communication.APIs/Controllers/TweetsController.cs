using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Star_Communications.APIs.Errors;
using Star_Communications.Core.Dtos;
using Star_Communications.Core.Services;
using System.Security.Claims;

namespace Star_Communications.APIs.Controllers
{
	[Authorize]
	public class TweetsController : BaseApiController
	{

		private readonly ITweetService _tweetService;


		public TweetsController(ITweetService tweetService)
		{
			_tweetService = tweetService;
		}

		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<ActionResult> CreateTweet(TweetDto tweetDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var errors = await _tweetService.CreateTweet(userId, tweetDto);

			if (errors.Length == 0)
				return CreatedAtAction(nameof(CreateTweet), new { message = "Tweet Created Successfully" });


			return BadRequest(new ApiValidationErrorResponse()
			{
				Errors = new string[] { errors }
			});
		}
	}
}
