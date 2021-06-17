using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Data.Migarations
{
    public partial class OrderModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActualOrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualOrderStatus",
                table: "Orders");
        }
    }
}
