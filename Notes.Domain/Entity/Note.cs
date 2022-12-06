using Notes.Domain.Base;
using System.Drawing;

namespace Notes.Domain.Entity
{
	public class Note : BaseEntity
	{
		public string Title { get; set; } = String.Empty;
		public int UserId { get; set; }
		public int Id { get; set; }
	}
}