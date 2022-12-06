using Notes.Persistence;
using static Notes.Persistent.DependencyInjection.DbDependencyInjection;
using static Notes.Persistent.DependencyInjection.IdentityDependency;

namespace Notes.Api
{
	public class Program
	{
		public IConfiguration Configuration { get; }

		public Program(IConfiguration configuration) => Configuration = configuration;
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbDependency(builder.Configuration);
			builder.Services.AddIdentityDependency();

			WebApplication app = builder.Build();

			app.Services.CreateScope().ServiceProvider.GetRequiredService<NotesContext>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}