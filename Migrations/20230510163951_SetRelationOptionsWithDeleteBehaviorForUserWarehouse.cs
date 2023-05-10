using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class SetRelationOptionsWithDeleteBehaviorForUserWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_User_UserId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Warehouse_WarehouseFromId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Warehouse_WarehouseToId",
                table: "Document");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_User_UserId",
                table: "Document",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Warehouse_WarehouseFromId",
                table: "Document",
                column: "WarehouseFromId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Warehouse_WarehouseToId",
                table: "Document",
                column: "WarehouseToId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_User_UserId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Warehouse_WarehouseFromId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Warehouse_WarehouseToId",
                table: "Document");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_User_UserId",
                table: "Document",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Warehouse_WarehouseFromId",
                table: "Document",
                column: "WarehouseFromId",
                principalTable: "Warehouse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Warehouse_WarehouseToId",
                table: "Document",
                column: "WarehouseToId",
                principalTable: "Warehouse",
                principalColumn: "Id");
        }
    }
}
