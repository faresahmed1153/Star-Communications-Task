using Stars_Communication.Core.Models.Identity;
using Stars_Communication.Core.Repositories;

namespace Stars_Communication.Repository.Repositories
{
	public class UserRepository : IUserRepository
	{

		private readonly AppDbContext _dbContext;


		public UserRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}


		public async Task<ApplicationUser> GetByIdAsync(string id)
			=> await _dbContext.Users.FindAsync(id);



	}
}
