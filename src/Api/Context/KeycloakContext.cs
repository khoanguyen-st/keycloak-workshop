using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Context
{
	public class KeycloakContext : DbContext
	{
		public KeycloakContext()
		{
		}

		public KeycloakContext(DbContextOptions<KeycloakContext> options)
		: base(options)
		{
		}

		public virtual DbSet<Permission> Permission { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Generate 10 seed data for Permission entity

			modelBuilder.Entity<Permission>().HasData(
				new Permission { Id = 1, Name = "create-user" },
				new Permission { Id = 2, Name = "view-user" },
				new Permission { Id = 3, Name = "delete-user" },
				new Permission { Id = 4, Name = "update-user" },
				new Permission { Id = 5, Name = "create-post" },
				new Permission { Id = 6, Name = "view-post" },
				new Permission { Id = 7, Name = "delete-post" },
				new Permission { Id = 8, Name = "update-post" }
																																																	);

			base.OnModelCreating(modelBuilder);
		}
	}
}
