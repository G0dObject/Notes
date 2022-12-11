using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Interfaces;
using Notes.Domain.Entity.Authorization;


namespace Notes.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly INotesDbContext context;
		public TestController(UserManager<User> userManager, INotesDbContext notesDb)
		{
			this._userManager = userManager;
			context = notesDb;
		}
		[Authorize]
		[HttpGet]
		public void GetSecret()
		{
			System.Security.Principal.IIdentity? f = User.Identity;
			System.Security.Claims.ClaimsPrincipal c = HttpContext.User;
		}
	}
}