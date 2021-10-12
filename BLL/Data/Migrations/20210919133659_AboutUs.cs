using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Data.Migrations
{
    public partial class AboutUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EducativeType",
                table: "Educatives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducativeType",
                table: "Educatives");
        }
    }
}
