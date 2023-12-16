using Microsoft.AspNetCore.Mvc;
using Star_Communications.APIs.Errors;
using Star_Communications.Core;
using Star_Communications.Core.Services;
using Star_Communications.Repository;
using Star_Communications.Service;
using Star_Communications.Service.Helpers;

namespace Star_Communications.APIs.Extentions
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
