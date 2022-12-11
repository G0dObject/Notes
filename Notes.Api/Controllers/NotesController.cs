using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

		private IsOwner<User, Note> _isowner;
		public NotesController(UserManager<User> userManager, INotesDbContext context)
		{
			_userManager = userManager;
			_context = context;
			_isowner = new(userManager, context);

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
		public async Task EditPost(int noteid, CreateNote noteupt)
		{
			bool isown = await _isowner.Check(User, noteid);

			Note? note = await _context!.Notes.FirstOrDefaultAsync(a => a.Id == noteid);
			User? user = await _userManager.GetUserAsync(User);


			note!.Text = noteupt.Text;
			note!.Title = noteupt.Title;

			_context!.Notes!.Update(note);
			await _context!.SaveChangesAsync(new CancellationToken());
			if (isown)
				Console.WriteLine("Pizda");
			;
		}
	}
}
