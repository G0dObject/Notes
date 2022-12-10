using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Interfaces;
using Notes.Domain.Entity.Authorization;


namespace Notes.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "Something")]
	public class TestController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly INotesDbContext context;
		public TestController(UserManager<User> userManager, INotesDbContext notesDb)
		{
			this._userManager = userManager;
			context = notesDb;

		}
		[HttpGet]
		public void Get()
		{
			_ = _userManager.CreateAsync(new User() { UserName = "gfdgdfgdfg", PasswordHash = "fdfgdfgdfgdfgff" });
		}
	}
}