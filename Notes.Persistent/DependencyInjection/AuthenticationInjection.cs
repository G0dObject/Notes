using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Notes.Persistent.DependencyInjection
{
	public static class AuthenticationInjection
	{
		public static IServiceCollection AddAuthenticationDependency(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.SaveToken = true;
				o.RequireHttpsMetadata = true;
				o.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidAudience = configuration["Jwt:Audience"],
					ValidIssuer = configuration["Jwt:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new Exception())),
					ValidateLifetime = true
				};
			});
			return services;
		}
	}
}
