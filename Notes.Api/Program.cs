using Notes.Api.Services;
using Notes.Application.Interfaces.Servises;
using Notes.Persistent.DependencyInjection;
using static Notes.Persistent.DependencyInjection.DbDependencyInjection;
using static Notes.Persistent.DependencyInjection.IdentityInjection;

namespace Notes.Api
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbDependency(builder.Configuration);
			builder.Services.AddIdentityDependency();
			builder.Services.AddAuthenticationDependency(builder.Configuration);

			builder.Services.AddAuthorizationBuilderDependency();

			builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
			WebApplication app = builder.Build();


			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.MapGet("/", () => "Ok");
			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}