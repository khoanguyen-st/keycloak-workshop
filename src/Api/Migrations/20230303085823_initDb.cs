using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
	/// <inheritdoc />
	public partial class initDb : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Name = table.Column<string>(type: "text", nullable: false),
					Email = table.Column<string>(type: "text", nullable: false),
					Phone = table.Column<string>(type: "text", nullable: false),
					Address = table.Column<string>(type: "text", nullable: false),
					Age = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "Users",
				columns: new[] { "Id", "Address", "Age", "Email", "Name", "Phone" },
				values: new object[,]
				{
					{ 1, "Danang", 18, "khoa@gmail.com", "khoanguyen", "0763602013" },
					{ 2, "HCM", 17, "bin@gmail.com", "binnguyen", "012356789" },
					{ 3, "Hanoi", 30, "nhat@gmail.com", "nhattruong", "0123321231" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
