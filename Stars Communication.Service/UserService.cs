using AutoMapper;
using Star_Communications.Core;
using Star_Communications.Core.Dtos;
using Star_Communications.Core.Services;

namespace Star_Communications.Service
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UserService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<UserProfileDto> GetUserProfile(string userId)
		{
			var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);

			var numOfFollowings = await _unitOfWork.UserFollowRepo.GetCountOfFollowingsAsync(userId);

			var numOfFollowers = await _unitOfWork.UserFollowRepo.GetCountOfFollowersAsync(userId);

			var userProfileDto = new UserProfileDto()
			{

				Name = user.Name,
				Email = user.Email,
				UserName = user.UserName,
				NumOfFollowers = numOfFollowers,
				NumOfFollowings = numOfFollowings,
			};

			return userProfileDto;
		}


	}
}
