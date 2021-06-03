using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Data.Migarations
{
    public partial class PictureUrlUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductUrl",
                table: "OrderItems",
                newName: "ItemOrdered_PictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemOrdered_PictureUrl",
                table: "OrderItems",
                newName: "ItemOrdered_ProductUrl");
        }
    }
}
