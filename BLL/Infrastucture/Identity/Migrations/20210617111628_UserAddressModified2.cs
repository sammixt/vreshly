using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Infrastucture.Identity.Migrations
{
    public partial class UserAddressModified2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropIndex(
            //     name: "IX_Address_AppUserId",
            //     table: "Address");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Address_AppUserId",
            //     table: "Address",
            //     column: "AppUserId",
            //     unique: true,
            //     filter: "[AppUserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_AppUserId",
                table: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AppUserId",
                table: "Address",
                column: "AppUserId");
        }
    }
}
