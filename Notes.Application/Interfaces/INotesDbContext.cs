using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entity;

namespace Notes.Application.Interfaces
{
	public interface INotesDbContext
	{
		DbSet<Note>? Notes { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
