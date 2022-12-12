using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain.Base;
using Notes.Domain.Entity.Authorization;
using Notes.Persistence;
using System.Security.Claims;

namespace Notes.Api.Services
{
	public class IsOwner<TUser, TObject> where TUser : class where TObject : class, IBaseEntityOneTo
	{
		private readonly UserManager<User> _userManager;
		private readonly NotesContext _context;
		public IsOwner(UserManager<User> userManager, INotesDbContext context)
		{
			_userManager = userManager;
			_context = (NotesContext)context;
		}
		public async Task<bool> Check(ClaimsPrincipal claims, int id)
		{
			TObject? obj = await _context.Set<TObject>().FirstOrDefaultAsync(a => a.Id == id);
			User? user = await _userManager.GetUserAsync(claims);

			if (obj == null || user == null)
				return false;
			else if (obj.UserId == user.Id)
				return true;
			return false;
		}
	}
}