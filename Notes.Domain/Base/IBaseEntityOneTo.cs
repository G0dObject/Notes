using Notes.Domain.Entity.Authorization;

namespace Notes.Domain.Base
{
	public interface IBaseEntityOneTo : IBaseEntity
	{
		public User? User { get; set; }
		public int UserId { get; set; }
	}
}
