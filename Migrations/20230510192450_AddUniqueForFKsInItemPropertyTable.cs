using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class AddUniqueForFKsInItemPropertyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemProperty_ItemId",
                table: "ItemProperty");

            migrationBuilder.CreateIndex(
                name: "IX_ItemProperty_ItemId_PropertyId",
                table: "ItemProperty",
                columns: new[] { "ItemId", "PropertyId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemProperty_ItemId_PropertyId",
                table: "ItemProperty");

            migrationBuilder.CreateIndex(
                name: "IX_ItemProperty_ItemId",
                table: "ItemProperty",
                column: "ItemId");
        }
    }
}
