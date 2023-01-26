using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.CreateTable(
			name: "Users",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				UserName = table.Column<string>(type: "TEXT", nullable: true),
				PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
				PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true),
				DateOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false),
				KnownAs = table.Column<string>(type: "TEXT", nullable: true),
				Created = table.Column<DateTime>(type: "TEXT", nullable: false),
				LastActive = table.Column<DateTime>(type: "TEXT", nullable: false),
				Gender = table.Column<string>(type: "TEXT", nullable: true),
				Introduction = table.Column<string>(type: "TEXT", nullable: true),
				LookingFor = table.Column<string>(type: "TEXT", nullable: true),
				Interests = table.Column<string>(type: "TEXT", nullable: true),
				City = table.Column<string>(type: "TEXT", nullable: true),
				Country = table.Column<string>(type: "TEXT", nullable: true)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_Users", x => x.Id);
			});

		_ = migrationBuilder.CreateTable(
			name: "Photos",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				Url = table.Column<string>(type: "TEXT", nullable: true),
				IsMain = table.Column<bool>(type: "INTEGER", nullable: false),
				PublicId = table.Column<string>(type: "TEXT", nullable: true),
				AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_Photos", x => x.Id);
				_ = table.ForeignKey(
					name: "FK_Photos_Users_AppUserId",
					column: x => x.AppUserId,
					principalTable: "Users",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		_ = migrationBuilder.CreateIndex(
			name: "IX_Photos_AppUserId",
			table: "Photos",
			column: "AppUserId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.DropTable(
			name: "Photos");

		_ = migrationBuilder.DropTable(
			name: "Users");
	}
}
