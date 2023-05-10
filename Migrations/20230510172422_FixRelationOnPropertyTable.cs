using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class FixRelationOnPropertyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemProperty_Property_PropertyId",
                table: "ItemProperty");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProperty_Property_PropertyId",
                table: "ItemProperty",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemProperty_Property_PropertyId",
                table: "ItemProperty");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProperty_Property_PropertyId",
                table: "ItemProperty",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
