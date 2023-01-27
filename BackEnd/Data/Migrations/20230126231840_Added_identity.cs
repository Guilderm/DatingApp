using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations;

/// <inheritdoc />
public partial class Addedidentity : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.DropForeignKey(
			name: "FK_Likes_Users_SourceUserId",
			table: "Likes");

		_ = migrationBuilder.DropForeignKey(
			name: "FK_Likes_Users_TargetUserId",
			table: "Likes");

		_ = migrationBuilder.DropForeignKey(
			name: "FK_Messages_Users_RecipientId",
			table: "Messages");

		_ = migrationBuilder.DropForeignKey(
			name: "FK_Messages_Users_SenderId",
			table: "Messages");

		_ = migrationBuilder.DropForeignKey(
			name: "FK_Photos_Users_AppUserId",
			table: "Photos");

		_ = migrationBuilder.DropPrimaryKey(
			name: "PK_Users",
			table: "Users");

		_ = migrationBuilder.DropColumn(
			name: "PasswordSalt",
			table: "Users");

		_ = migrationBuilder.RenameTable(
			name: "Users",
			newName: "AspNetUsers");

		_ = migrationBuilder.AlterColumn<string>(
			name: "PasswordHash",
			table: "AspNetUsers",
			type: "TEXT",
			nullable: true,
			oldClrType: typeof(byte[]),
			oldType: "BLOB",
			oldNullable: true);

		_ = migrationBuilder.AddColumn<int>(
			name: "AccessFailedCount",
			table: "AspNetUsers",
			type: "INTEGER",
			nullable: false,
			defaultValue: 0);

		_ = migrationBuilder.AddColumn<string>(
			name: "ConcurrencyStamp",
			table: "AspNetUsers",
			type: "TEXT",
			nullable: true);

		_ = migrationBuilder.AddColumn<string>(
			name: "Email",
			table: "AspNetUsers",
			type: "TEXT",
			maxLength: 256,
			nullable: true);

		_ = migrationBuilder.AddColumn<bool>(
			name: "EmailConfirmed",
			table: "AspNetUsers",
			type: "INTEGER",
			nullable: false,
			defaultValue: false);

		_ = migrationBuilder.AddColumn<bool>(
			name: "LockoutEnabled",
			table: "AspNetUsers",
			type: "INTEGER",
			nullable: false,
			defaultValue: false);

		_ = migrationBuilder.AddColumn<DateTimeOffset>(
			name: "LockoutEnd",
			table: "AspNetUsers",
			type: "TEXT",
			nullable: true);

		_ = migrationBuilder.AddColumn<string>(
			name: "NormalizedEmail",
			table: "AspNetUsers",
			type: "TEXT",
			maxLength: 256,
			nullable: true);

		_ = migrationBuilder.AddColumn<string>(
			name: "NormalizedUserName",
			table: "AspNetUsers",
			type: "TEXT",
			maxLength: 256,
			nullable: true);

		_ = migrationBuilder.AddColumn<string>(
			name: "PhoneNumber",
			table: "AspNetUsers",
			type: "TEXT",
			nullable: true);

		_ = migrationBuilder.AddColumn<bool>(
			name: "PhoneNumberConfirmed",
			table: "AspNetUsers",
			type: "INTEGER",
			nullable: false,
			defaultValue: false);

		_ = migrationBuilder.AddColumn<string>(
			name: "SecurityStamp",
			table: "AspNetUsers",
			type: "TEXT",
			nullable: true);

		_ = migrationBuilder.AddColumn<bool>(
			name: "TwoFactorEnabled",
			table: "AspNetUsers",
			type: "INTEGER",
			nullable: false,
			defaultValue: false);

		_ = migrationBuilder.AddPrimaryKey(
			name: "PK_AspNetUsers",
			table: "AspNetUsers",
			column: "Id");

		_ = migrationBuilder.CreateTable(
			name: "AspNetRoles",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
				NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_AspNetRoles", x => x.Id);
			});

		_ = migrationBuilder.CreateTable(
			name: "AspNetUserClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				UserId = table.Column<int>(type: "INTEGER", nullable: false),
				ClaimType = table.Column<string>(type: "TEXT", nullable: true),
				ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
				_ = table.ForeignKey(
					name: "FK_AspNetUserClaims_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		_ = migrationBuilder.CreateTable(
			name: "AspNetUserLogins",
			columns: table => new
			{
				LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
				ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
				ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
				UserId = table.Column<int>(type: "INTEGER", nullable: false)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
				_ = table.ForeignKey(
					name: "FK_AspNetUserLogins_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		_ = migrationBuilder.CreateTable(
			name: "AspNetUserTokens",
			columns: table => new
			{
				UserId = table.Column<int>(type: "INTEGER", nullable: false),
				LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
				Name = table.Column<string>(type: "TEXT", nullable: false),
				Value = table.Column<string>(type: "TEXT", nullable: true)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
				_ = table.ForeignKey(
					name: "FK_AspNetUserTokens_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		_ = migrationBuilder.CreateTable(
			name: "AspNetRoleClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				RoleId = table.Column<int>(type: "INTEGER", nullable: false),
				ClaimType = table.Column<string>(type: "TEXT", nullable: true),
				ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
				_ = table.ForeignKey(
					name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		_ = migrationBuilder.CreateTable(
			name: "AspNetUserRoles",
			columns: table => new
			{
				UserId = table.Column<int>(type: "INTEGER", nullable: false),
				RoleId = table.Column<int>(type: "INTEGER", nullable: false)
			},
			constraints: table =>
			{
				_ = table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
				_ = table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				_ = table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		_ = migrationBuilder.CreateIndex(
			name: "EmailIndex",
			table: "AspNetUsers",
			column: "NormalizedEmail");

		_ = migrationBuilder.CreateIndex(
			name: "UserNameIndex",
			table: "AspNetUsers",
			column: "NormalizedUserName",
			unique: true);

		_ = migrationBuilder.CreateIndex(
			name: "IX_AspNetRoleClaims_RoleId",
			table: "AspNetRoleClaims",
			column: "RoleId");

		_ = migrationBuilder.CreateIndex(
			name: "RoleNameIndex",
			table: "AspNetRoles",
			column: "NormalizedName",
			unique: true);

		_ = migrationBuilder.CreateIndex(
			name: "IX_AspNetUserClaims_UserId",
			table: "AspNetUserClaims",
			column: "UserId");

		_ = migrationBuilder.CreateIndex(
			name: "IX_AspNetUserLogins_UserId",
			table: "AspNetUserLogins",
			column: "UserId");

		_ = migrationBuilder.CreateIndex(
			name: "IX_AspNetUserRoles_RoleId",
			table: "AspNetUserRoles",
			column: "RoleId");

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Likes_AspNetUsers_SourceUserId",
			table: "Likes",
			column: "SourceUserId",
			principalTable: "AspNetUsers",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Likes_AspNetUsers_TargetUserId",
			table: "Likes",
			column: "TargetUserId",
			principalTable: "AspNetUsers",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Messages_AspNetUsers_RecipientId",
			table: "Messages",
			column: "RecipientId",
			principalTable: "AspNetUsers",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Messages_AspNetUsers_SenderId",
			table: "Messages",
			column: "SenderId",
			principalTable: "AspNetUsers",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Photos_AspNetUsers_AppUserId",
			table: "Photos",
			column: "AppUserId",
			principalTable: "AspNetUsers",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.DropForeignKey(
			name: "FK_Likes_AspNetUsers_SourceUserId",
			table: "Likes");

		_ = migrationBuilder.DropForeignKey(
			name: "FK_Likes_AspNetUsers_TargetUserId",
			table: "Likes");

		_ = migrationBuilder.DropForeignKey(
			name: "FK_Messages_AspNetUsers_RecipientId",
			table: "Messages");

		_ = migrationBuilder.DropForeignKey(
			name: "FK_Messages_AspNetUsers_SenderId",
			table: "Messages");

		_ = migrationBuilder.DropForeignKey(
			name: "FK_Photos_AspNetUsers_AppUserId",
			table: "Photos");

		_ = migrationBuilder.DropTable(
			name: "AspNetRoleClaims");

		_ = migrationBuilder.DropTable(
			name: "AspNetUserClaims");

		_ = migrationBuilder.DropTable(
			name: "AspNetUserLogins");

		_ = migrationBuilder.DropTable(
			name: "AspNetUserRoles");

		_ = migrationBuilder.DropTable(
			name: "AspNetUserTokens");

		_ = migrationBuilder.DropTable(
			name: "AspNetRoles");

		_ = migrationBuilder.DropPrimaryKey(
			name: "PK_AspNetUsers",
			table: "AspNetUsers");

		_ = migrationBuilder.DropIndex(
			name: "EmailIndex",
			table: "AspNetUsers");

		_ = migrationBuilder.DropIndex(
			name: "UserNameIndex",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "AccessFailedCount",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "ConcurrencyStamp",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "Email",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "EmailConfirmed",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "LockoutEnabled",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "LockoutEnd",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "NormalizedEmail",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "NormalizedUserName",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "PhoneNumber",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "PhoneNumberConfirmed",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "SecurityStamp",
			table: "AspNetUsers");

		_ = migrationBuilder.DropColumn(
			name: "TwoFactorEnabled",
			table: "AspNetUsers");

		_ = migrationBuilder.RenameTable(
			name: "AspNetUsers",
			newName: "Users");

		_ = migrationBuilder.AlterColumn<byte[]>(
			name: "PasswordHash",
			table: "Users",
			type: "BLOB",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "TEXT",
			oldNullable: true);

		_ = migrationBuilder.AddColumn<byte[]>(
			name: "PasswordSalt",
			table: "Users",
			type: "BLOB",
			nullable: true);

		_ = migrationBuilder.AddPrimaryKey(
			name: "PK_Users",
			table: "Users",
			column: "Id");

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Likes_Users_SourceUserId",
			table: "Likes",
			column: "SourceUserId",
			principalTable: "Users",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Likes_Users_TargetUserId",
			table: "Likes",
			column: "TargetUserId",
			principalTable: "Users",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Messages_Users_RecipientId",
			table: "Messages",
			column: "RecipientId",
			principalTable: "Users",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Messages_Users_SenderId",
			table: "Messages",
			column: "SenderId",
			principalTable: "Users",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);

		_ = migrationBuilder.AddForeignKey(
			name: "FK_Photos_Users_AppUserId",
			table: "Photos",
			column: "AppUserId",
			principalTable: "Users",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}
}
