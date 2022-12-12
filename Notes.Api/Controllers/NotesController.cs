using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Builders;
using Notes.Api.Services;
using Notes.Application.Common.Note;
using Notes.Application.Interfaces;
using Notes.Domain.Entity;
using Notes.Domain.Entity.Authorization;

namespace Notes.Api.Controllers
{
	[Authorize()]

	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly INotesDbContext _context;
		private IsOwner<User, Note> _owner;
		public NotesController(UserManager<User> userManager, INotesDbContext context)
		{
			_userManager = userManager;
			_context = context;
			_owner = new(userManager, context);
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

			await _context.SaveChangesAsync(new CancellationToken());
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> EditPost(int noteid, CreateNote noteupt)
		{
			if (!await _owner.Check(User, noteid))
				return StatusCode(StatusCodes.Status404NotFound);

			Note? note = await _context!.Notes!.FirstOrDefaultAsync(a => a.Id == noteid);
			User? user = await _userManager.GetUserAsync(User);

			note!.Text = noteupt.Text;
			note!.Title = noteupt.Title;

			_context!.Notes!.Update(note);
			await _context!.SaveChangesAsync(new CancellationToken());
			return StatusCode(StatusCodes.Status200OK);
		}

		[HttpDelete]
		public async Task<IActionResult> DeletePost(int noteid)
		{
			if (!await _owner.Check(User, noteid))
				return StatusCode(StatusCodes.Status404NotFound);

			_context!.Notes!.Remove(await _context!.Notes!.FindAsync(noteid));
			await _context!.SaveChangesAsync(new CancellationToken());
			return StatusCode(StatusCodes.Status200OK);
		}
	}
}
