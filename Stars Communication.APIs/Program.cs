using Microsoft.EntityFrameworkCore;
using Star_Communications.APIs.Extentions;
using Star_Communications.APIs.Middlewares;
using Star_Communications.Repository;
using System.Text.Json.Serialization;

namespace Star_Communications.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure Services

			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			});


			builder.Services.AddSwaggerServices();

			builder.Services.AddDbContext<AppDbContext>(options =>

			{
				options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddApplicationServices(builder.Configuration);

			builder.Services.AddIdentityServices(builder.Configuration);

			builder.Services.AddCors(options =>
			options.AddPolicy("MyPolicy", options =>
			options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
			);

			#endregion



			var app = builder.Build();



			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{

				var dbContext = services.GetRequiredService<AppDbContext>();

				await dbContext.Database.MigrateAsync();

			}
			catch (Exception ex)
			{

				var logger = loggerFactory.CreateLogger<Program>();

				logger.LogError(ex, "an error occured during apply the migrations");
			}



			// Configure the HTTP request pipeline.

			#region Configure Kestrel Middlewares

			app.UseMiddleware<ExceptionMiddleware>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddleWares();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();

			app.UseCors("MyPolicy");

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			#endregion


			app.Run();
		}
	}
}
