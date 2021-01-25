using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.JumpAppDb
{
    public partial class UpdatedUserInformationTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "UserInformation");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserInformation",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Rank",
                table: "UserInformation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "UserInformation");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserInformation",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "UserInformation",
                nullable: true);
        }
    }
}
