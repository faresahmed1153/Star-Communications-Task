﻿using Stars_Communication.Core.Models;
using Stars_Communication.Core.Repositories;

namespace Stars_Communication.Repository.Repositories
{
	public class TweetRepository<T> : IGenericRepository<T> where T : Tweet
	{

		private readonly AppDbContext _dbContext;


		public TweetRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}


		public async Task AddAsync(T entity)

		=> await _dbContext.Set<T>().AddAsync(entity);

	}
}
