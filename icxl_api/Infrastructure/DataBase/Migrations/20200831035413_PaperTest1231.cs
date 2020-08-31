using Microsoft.EntityFrameworkCore.Migrations;

namespace icxl_api.Migrations
{
    public partial class PaperTest1231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Account",
                newName: "PassWord");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Account",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    url = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    parentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.RenameColumn(
                name: "PassWord",
                table: "Account",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Account",
                newName: "name");
        }
    }
}
