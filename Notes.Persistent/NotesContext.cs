using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Notes.Application.Interfaces;
using Notes.Domain.Entity;
using Notes.Domain.Entity.Authorization;
using Notes.Persistence.EntityTypeConfigurations;
using Notes.Persistent;

namespace Notes.Persistence
{
	public class NotesContext : IdentityDbContext<User, Role, int>, INotesDbContext
	{
		public NotesContext(DbContextOptions<NotesContext> contextOptions) : base(contextOptions) { DbInitialize.Initialize(this); }

		public DbSet<Note>? Notes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new NoteConfigurations());
			base.OnModelCreating(modelBuilder);
		}
	}
}
