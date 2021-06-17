using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Infrastucture.Identity.Migrations
{
    public partial class UpdatePhoneInIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Address");
        }
    }
}
