using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.JumpAppDb
{
    public partial class AddedUserName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserInformation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserInformation");
        }
    }
}
