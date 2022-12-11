using Notes.Domain.Entity;
using Notes.Domain.Entity.Authorization;

namespace Notes.Application.Interfaces.Builders
{
	public interface INoteBuilder
	{
		public INoteBuilder AddTitle(string title);
		public INoteBuilder AddText(string text);
		public INoteBuilder AddUser(User user);
		public Note GetNote();
	}
}
