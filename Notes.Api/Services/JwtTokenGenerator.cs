using Microsoft.IdentityModel.Tokens;
using Notes.Application.Interfaces.Servises;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Notes.Api.Services
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private readonly IConfiguration _configuration;
		public JwtTokenGenerator(IConfiguration configuration) => _configuration = configuration;

		public JwtSecurityToken GenerateJwtToken(List<Claim> claims)
		{
			SymmetricSecurityKey? authSigningKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ConfigurationErrorsException()));
			JwtSecurityToken? token = new(
				issuer: _configuration["Jwt:Issuer"] ?? throw new ConfigurationErrorsException(),
				audience: _configuration["Jwt:Audience"] ?? throw new ConfigurationErrorsException(),
				expires: DateTime.Now.AddSeconds(double.Parse(_configuration["Jwt:TokenLifeTime"] ?? throw new ConfigurationErrorsException())),
				claims: claims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);
			return token;
		}
	}
}