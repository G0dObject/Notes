using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;
using Notes.Persistence;
using System.Configuration;
using System.Data;

namespace Notes.Persistent.DependencyInjection
{
	public static class DbDependencyInjection
	{
		public static IServiceCollection AddDbDependency(this IServiceCollection services, IConfiguration configuration)
		{
			string _connectionString = configuration.GetConnectionString("Sqlite") ?? throw new ConfigurationErrorsException();
			_ = services.AddDbContext<NotesContext>(opt => { _ = opt.UseSqlite(_connectionString); });

			services.AddScoped<INotesDbContext>(provider =>
			   provider.GetService<NotesContext>() ?? new NotesContext(new DbContextOptions<NotesContext>()));


			return services;
		}

	}
}

