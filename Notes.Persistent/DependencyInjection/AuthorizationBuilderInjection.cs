using Microsoft.Extensions.DependencyInjection;

namespace Notes.Persistent.DependencyInjection
{
	public static class AuthorizationBuilderInjection
	{
		public static IServiceCollection AddAuthorizationBuilderDependency(this IServiceCollection services)
		{
			services.AddAuthorizationBuilder().AddPolicy("Something", p => { p.RequireUserName("Admin"); });

			return services;
		}
	}
}
