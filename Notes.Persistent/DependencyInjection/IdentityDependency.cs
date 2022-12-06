﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Notes.Domain.Entity.Authorization;
using Notes.Persistence;

namespace Notes.Persistent.DependencyInjection
{
	public static class IdentityDependency
	{
		public static IServiceCollection AddIdentityDependency(this IServiceCollection services)
		{
			IdentityBuilder? builder = services.AddIdentity<User, Role>(option =>
			{
				option.User.RequireUniqueEmail = false;

				option.Stores.MaxLengthForKeys = 128;

				option.Password.RequireUppercase = false;
				option.Password.RequireNonAlphanumeric = false;
				option.Password.RequireDigit = false;

				option.SignIn.RequireConfirmedPhoneNumber = false;
				option.SignIn.RequireConfirmedEmail = false;
				option.SignIn.RequireConfirmedAccount = false;
			}).AddEntityFrameworkStores<NotesContext>().AddDefaultTokenProviders();

			return services;
		}
	}
}
