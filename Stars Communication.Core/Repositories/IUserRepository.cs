using Star_Communications.Core.Models.Identity;

namespace Star_Communications.Core.Repositories
{
	public interface IUserRepository
	{

		Task<ApplicationUser> GetByIdAsync(string id);

	}
}
