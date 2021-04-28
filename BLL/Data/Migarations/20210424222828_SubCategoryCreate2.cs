using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Data.Migarations
{
    public partial class SubCategoryCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "SubCategories",
                newName: "SubCategoryName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategoryName",
                table: "SubCategories",
                newName: "CategoryName");
        }
    }
}
