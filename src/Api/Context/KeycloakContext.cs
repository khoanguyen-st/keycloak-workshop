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

		public virtual DbSet<Post> Post { get; set; }

		public virtual DbSet<Comment> Comment { get; set; }

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
				new Permission { Id = 8, Name = "update-post" });

			//Generate 10 seed data for Post entity
			modelBuilder.Entity<Post>().HasData(
				new Post { Id = 1, Title = "Post 1", Content = "Content 1", Author = "admin", Type = "public" },
				new Post { Id = 2, Title = "Post 2", Content = "Content 2", Author = "admin", Type = "public" },
				new Post { Id = 3, Title = "Post 3", Content = "Content 3", Author = "admin", Type = "public" },
				new Post { Id = 4, Title = "Post 4", Content = "Content 4", Author = "admin", Type = "public" },
				new Post { Id = 5, Title = "Post 5", Content = "Content 5", Author = "admin", Type = "public" });

			base.OnModelCreating(modelBuilder);
		}
	}
}
