using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stars_Communication.APIs.Errors;
using Stars_Communication.Core.Dtos;
using Stars_Communication.Core.Models.Identity;
using Stars_Communication.Core.Services;
using System.Security.Claims;

namespace Stars_Communication.APIs.Controllers
{

	public class UsersController : BaseApiController
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IUserFollowService _userFollowService;

		public UsersController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager,
			ITokenService tokenService,
			IMapper mapper,
			IUserService userService,
			IUserFollowService userFollowService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_tokenService = tokenService;
			_mapper = mapper;
			_userService = userService;
			_userFollowService = userFollowService;
		}

		[ProducesResponseType(typeof(UserToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
		[HttpPost("login")]
		public async Task<ActionResult<UserToReturnDto>> Login(LoginDto loginDto)
		{

			var user = await _userManager.FindByNameAsync(loginDto.UserName);

			if (user == null) return Unauthorized(new ApiResponse(401));

			var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

			if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

			return Ok(new UserToReturnDto()
			{

				UserName = loginDto.UserName,

				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			});
		}



		[ProducesResponseType(typeof(UserToReturnDto), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
		[HttpPost("register")]
		public async Task<ActionResult<UserToReturnDto>> Register(RegisterDto RegisterDto)
		{

			if (CheckUserNameExists(RegisterDto.UserName).Result.Value)

				return BadRequest(new ApiValidationErrorResponse()
				{
					Errors = new string[] { "UserName is already in use!" }
				});

			if (CheckEmailExists(RegisterDto.Email).Result.Value)

				return BadRequest(new ApiValidationErrorResponse()
				{
					Errors = new string[] { "email is already in use!" }
				});

			var mappedUser = _mapper.Map<RegisterDto, ApplicationUser>(RegisterDto);

			var creatingUser = await _userManager.CreateAsync(mappedUser, RegisterDto.Password);

			if (!creatingUser.Succeeded) return BadRequest(new ApiResponse(400));

			var user = await _userManager.FindByNameAsync(RegisterDto.UserName);

			var userToReturnDto = new UserToReturnDto()
			{

				UserName = RegisterDto.UserName,

				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			};

			return CreatedAtAction(nameof(Register), userToReturnDto);
		}


		[ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
		[Authorize]
		[HttpGet("profile")]
		public async Task<ActionResult<UserProfileDto>> GetUserProfile()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			return Ok(await _userService.GetUserProfile(userId));
		}


		[ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
		[Authorize]
		[HttpDelete("{id}/unfollow")]
		public async Task<ActionResult<UserProfileDto>> UnFollowUser(string id)
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var errors = await _userFollowService.UnFollowUser(currentUserId, id);

			if (errors.Length == 0)
				return Ok(new { message = $"You have unfollowed the user with Id: {id} successfully" });


			return BadRequest(new ApiValidationErrorResponse()
			{
				Errors = new string[] { errors }
			});
		}


		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
		[Authorize]
		[HttpPost("{id}/follow")]
		public async Task<ActionResult<UserProfileDto>> FollowUser(string id)
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var errors = await _userFollowService.FollowUser(currentUserId, id);

			if (errors.Length == 0)
				return Ok(new { message = $"You have followed the user with Id: {id} successfully" });


			return BadRequest(new ApiValidationErrorResponse()
			{
				Errors = new string[] { errors }
			});
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("emailExists")]
		public async Task<ActionResult<bool>> CheckEmailExists(string email)
		{
			return await _userManager.FindByEmailAsync(email) is not null;
		}


		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("userNameExists")]
		public async Task<ActionResult<bool>> CheckUserNameExists(string userName)
		{

			return await _userManager.FindByNameAsync(userName) is not null;
		}
	}
}
