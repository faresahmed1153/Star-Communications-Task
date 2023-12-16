using Stars_Communication.Core.Models.Identity;

namespace Stars_Communication.Core.Repositories
{
	public interface IUserRepository
	{

		Task<ApplicationUser> GetByIdAsync(string id);

	}
}
