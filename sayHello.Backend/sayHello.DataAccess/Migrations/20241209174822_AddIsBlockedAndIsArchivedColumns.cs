using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sayHello.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIsBlockedAndIsArchivedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "BlockedUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "ArchivedUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "BlockedUsers");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "ArchivedUsers");
        }
    }
}
