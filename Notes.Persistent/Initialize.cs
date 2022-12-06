using Notes.Persistence;

namespace Notes.Persistent
{
	internal class DbInitialize
	{
		public static void Initialize(NotesContext context)
		{
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
		}
	}
}