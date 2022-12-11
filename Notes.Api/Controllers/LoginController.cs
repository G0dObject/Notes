using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Common.User;
using Notes.Application.Interfaces.Servises;
using Notes.Domain.Entity.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Notes.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;

		public LoginController(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
		{
			_userManager = userManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		[Route("Login")]
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginUser model)
		{
			User? user = await _userManager.FindByEmailAsync(model.Email);

			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				IList<string> userRoles = await _userManager.GetRolesAsync(user);

				List<Claim> authClaims = new()
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, user!.UserName)
				};

				foreach (string? userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}
				JwtSecurityToken? token = _jwtTokenGenerator.GenerateJwtToken(authClaims);

				return Ok(new
				{
					username = user.UserName,
					token = new JwtSecurityTokenHandler().WriteToken(token),
				});
			}
			return Unauthorized();
		}

		[Route("Register")]
		[HttpPost]
		public async Task<IActionResult> Register([FromBody] CreateUser model)
		{
			User? existuser = await _userManager.FindByNameAsync(model.Name);
			if (existuser != null)
				return StatusCode(StatusCodes.Status409Conflict, "Alredy exist");

			User user = new()
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.Name,
			};
			IdentityResult result = await _userManager.CreateAsync(user, model.Password);

			return result.Succeeded
				? StatusCode(StatusCodes.Status201Created, "Created") :
				StatusCode(StatusCodes.Status400BadRequest, "Create Failed");
		}

	}
}