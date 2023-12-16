using Microsoft.AspNetCore.Mvc;
using Stars_Communication.APIs.Errors;
using Stars_Communication.Core;
using Stars_Communication.Core.Services;
using Stars_Communication.Repository;
using Stars_Communication.Service;
using Stars_Communication.Service.Helpers;

namespace Stars_Communication.APIs.Extentions
{
	public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddScoped<ITweetService, TweetService>();

			services.AddScoped<IUserService, UserService>();

			services.AddScoped<IUserFollowService, UserFollowService>();

			services.AddAutoMapper(typeof(MappingProfiles));

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
					.SelectMany(P => P.Value.Errors)
					.Select(E => E.ErrorMessage)
					.ToArray();
					var validationErrorResponse = new ApiValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(validationErrorResponse);
				};
			}
		   );
			return services;
		}
	}
}
