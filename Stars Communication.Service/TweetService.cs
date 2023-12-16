using AutoMapper;
using Star_Communications.Core;
using Star_Communications.Core.Dtos;
using Star_Communications.Core.Models;
using Star_Communications.Core.Services;

namespace Star_Communications.Service
{
	public class TweetService : ITweetService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;


		public TweetService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<string> CreateTweet(string userId, TweetDto tweetDto)
		{

			tweetDto.UserId = userId;

			var mappedTweet = _mapper.Map<TweetDto, Tweet>(tweetDto);

			await _unitOfWork.TweetRepo.AddAsync(mappedTweet);

			int affectedRows = await _unitOfWork.Complete();

			if (affectedRows > 0)
				return "";

			return "hmmm looks like creating the tweet failed!";
		}
	}
}
