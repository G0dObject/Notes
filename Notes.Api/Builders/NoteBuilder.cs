using Notes.Application.Interfaces.Builders;
using Notes.Domain.Entity;
using Notes.Domain.Entity.Authorization;

namespace Notes.Api.Builders
{
	internal class NoteBuilder : INoteBuilder
	{
		private Note buildnote = new();
		public INoteBuilder AddText(string text)
		{
			buildnote.Text = text;
			return this;
		}

		public INoteBuilder AddTitle(string title)
		{
			buildnote.Title = title;
			return this;
		}

		public INoteBuilder AddUser(User user)
		{
			buildnote.User = user;
			return this;
		}
		public Note GetNote() => buildnote;

	}
}
