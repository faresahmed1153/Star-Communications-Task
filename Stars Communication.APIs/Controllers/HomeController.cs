using Microsoft.AspNetCore.Mvc;
using Star_Communications.Core.Dtos;
using Star_Communications.Core.Services;
using System.Security.Claims;

namespace Star_Communications.APIs.Controllers
{

	public class HomeController : BaseApiController
	{
		private readonly IUserService _userService;
		private readonly IUserFollowService _userFollowService;

		public HomeController(IUserService userService, IUserFollowService userFollowService)
		{
			_userService = userService;
			_userFollowService = userFollowService;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetAllTweets([FromQuery] PaginationDto paginationDto)
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (currentUserId is not null)
			{

				return Ok(await _userFollowService.GetAllTweetsOfFollowings(currentUserId, paginationDto));
			}

			return Ok(await _userFollowService.GetTweetsForMostFollowedFiveUsers(paginationDto));
		}

	}
}
