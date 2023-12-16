using Microsoft.AspNetCore.Identity;
using Stars_Communication.Core.Models.Identity;

namespace Stars_Communication.Core.Services
{
	public interface ITokenService
	{
		Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
	}
}
