using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class SetRelationOptionsWithDeleteBehaviorForItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Line_Item_ItemId",
                table: "Line");

            migrationBuilder.AddForeignKey(
                name: "FK_Line_Item_ItemId",
                table: "Line",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Line_Item_ItemId",
                table: "Line");

            migrationBuilder.AddForeignKey(
                name: "FK_Line_Item_ItemId",
                table: "Line",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
