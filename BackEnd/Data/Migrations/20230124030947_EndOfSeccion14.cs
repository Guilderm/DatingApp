using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations;

/// <inheritdoc />
public partial class EndOfSeccion14 : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.CreateTable(
			name: "Likes",
			columns: table => new
			{
				SourceUserId = table.Column<int>(type: "INTEGER", nullable: false),
				TargetUserId = table.Column<int>(type: "INTEGER", nullable: false)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_Likes", x => new { x.SourceUserId, x.TargetUserId });
				_ = table.ForeignKey(
					name: "FK_Likes_Users_SourceUserId",
					column: x => x.SourceUserId,
					principalTable: "Users",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				_ = table.ForeignKey(
					name: "FK_Likes_Users_TargetUserId",
					column: x => x.TargetUserId,
					principalTable: "Users",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		_ = migrationBuilder.CreateIndex(
			name: "IX_Likes_TargetUserId",
			table: "Likes",
			column: "TargetUserId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.DropTable(
			name: "Likes");
	}
}
