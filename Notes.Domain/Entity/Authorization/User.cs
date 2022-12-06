using Microsoft.AspNetCore.Identity;
using Notes.Domain.Base;

namespace Notes.Domain.Entity.Authorization
{
	public class User : IdentityUser<int>, BaseEntity
	{
	}
}