using Star_Communications.Core.Models.Identity;
using Star_Communications.Core.Repositories;

namespace Star_Communications.Repository.Repositories
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
