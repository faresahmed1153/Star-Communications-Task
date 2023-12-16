using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Star_Communications.Core.Models.Identity;
using Star_Communications.Core.Services;
using Star_Communications.Repository;
using Star_Communications.Service;
using System.Text;

namespace Star_Communications.APIs.Extentions
{
	public static class IdentityServicesExtention
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped(typeof(ITokenService), typeof(TokenService));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			 {

			 }).AddEntityFrameworkStores<AppDbContext>();


			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["Jwt:ValidIssure"],
						ValidateAudience = true,
						ValidAudience = configuration["Jwt:ValidAudience"],
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))

					};
				});
			return services;
		}
	}
}
