using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Builders;
using Notes.Application.Common.Note;
using Notes.Application.Interfaces;
using Notes.Domain.Entity;
using Notes.Domain.Entity.Authorization;
using System.Security.Claims;

namespace Notes.Api.Controllers
{
	[Authorize()]

	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly INotesDbContext _context;

		public NotesController(UserManager<User> userManager, INotesDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return new JsonResult(_context.Notes!.Where(u => u.User!.UserName!.Equals(User.Identity!.Name)));
		}

		[HttpPost]
		public async Task<IActionResult> AddPost([FromBody] CreateNote createNote)
		{
			User? user = await _userManager.GetUserAsync(User);

			Note note = new NoteBuilder()
				.AddTitle(createNote.Title)
				.AddText(createNote.Text)
				.AddUser(user!)
				.GetNote();
			await _context!.Notes!.AddAsync(note);

			CancellationToken canToken = new CancellationToken();
			await _context.SaveChangesAsync(canToken);
			return Ok();
		}
		[HttpPut]
		public void EditPost()
		{
		}
	}
}
