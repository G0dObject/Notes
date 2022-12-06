using Notes.Domain.Base;
using Notes.Domain.Entity.Authorization;

namespace Notes.Domain.Entity
{
	public class Note : BaseEntity
	{
		public string Title { get; set; } = string.Empty;
		public int Id { get; set; }
		public virtual User? User { get; set; }
		public int UserId { get; set; }
	}
}