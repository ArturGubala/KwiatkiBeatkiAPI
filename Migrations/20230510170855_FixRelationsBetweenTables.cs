using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class FixRelationsBetweenTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_TradePartner_TradePartnerId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_BulkPack_BulkPackId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemType_ItemTypeId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_MeasurementUnit_MeasurementUnitId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Producer_ProducerId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                table: "Document",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_TradePartner_TradePartnerId",
                table: "Document",
                column: "TradePartnerId",
                principalTable: "TradePartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_BulkPack_BulkPackId",
                table: "Item",
                column: "BulkPackId",
                principalTable: "BulkPack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemType_ItemTypeId",
                table: "Item",
                column: "ItemTypeId",
                principalTable: "ItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_MeasurementUnit_MeasurementUnitId",
                table: "Item",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Producer_ProducerId",
                table: "Item",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_TradePartner_TradePartnerId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_BulkPack_BulkPackId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemType_ItemTypeId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_MeasurementUnit_MeasurementUnitId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Producer_ProducerId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                table: "Document",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_TradePartner_TradePartnerId",
                table: "Document",
                column: "TradePartnerId",
                principalTable: "TradePartner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_BulkPack_BulkPackId",
                table: "Item",
                column: "BulkPackId",
                principalTable: "BulkPack",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemType_ItemTypeId",
                table: "Item",
                column: "ItemTypeId",
                principalTable: "ItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_MeasurementUnit_MeasurementUnitId",
                table: "Item",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Producer_ProducerId",
                table: "Item",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
