using Microsoft.AspNetCore.Identity;
using Notes.Domain.Base;

namespace Notes.Domain.Entity.Authorization
{
	public class Role : IdentityRole<int>, BaseEntity
	{

	}
}
