using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Builders;
using Notes.Api.Services;
using Notes.Application.Common.Note;
using Notes.Application.Interfaces;
using Notes.Domain.Base;
using Notes.Domain.Entity;
using Notes.Domain.Entity.Authorization;
using Notes.Persistence;
using System.Security.Claims;

namespace Notes.Api.Services
{
	public class IsOwner<TUser, TObject> where TUser : IdentityUser<int> where TObject : class, BaseEntity
	{
		private readonly UserManager<User> _userManager;
		private readonly NotesContext _context;
		public IsOwner(UserManager<User> userManager, INotesDbContext context)
		{
			_userManager = userManager;
			_context = context as NotesContext;
		}
		public async Task<bool> Check(ClaimsPrincipal claims, int id)
		{
			TObject? obj = await _context.Set<TObject>().FirstOrDefaultAsync(a => a.Id == id);
			User? user = await _userManager.GetUserAsync(claims);

			if (obj.Id == user.Id)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}