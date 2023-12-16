using Microsoft.AspNetCore.Identity;
using Star_Communications.Core.Models.Identity;

namespace Star_Communications.Core.Services
{
	public interface ITokenService
	{
		Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
	}
}
