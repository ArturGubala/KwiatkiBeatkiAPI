using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class ChengeUniqueIndexFromNameToStockcodeInItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Item_Name",
                table: "Item");

            migrationBuilder.AlterColumn<string>(
                name: "StockCode",
                table: "Item",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_StockCode",
                table: "Item",
                column: "StockCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Item_StockCode",
                table: "Item");

            migrationBuilder.AlterColumn<string>(
                name: "StockCode",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Item",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_Name",
                table: "Item",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
