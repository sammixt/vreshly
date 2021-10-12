using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Data.Migrations
{
    public partial class BannerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitleOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitleTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitleThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleFour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitleFour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubPageImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");
        }
    }
}
