﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class FixRelationOnItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemProperty_Item_ItemId",
                table: "ItemProperty");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProperty_Item_ItemId",
                table: "ItemProperty",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemProperty_Item_ItemId",
                table: "ItemProperty");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemProperty_Item_ItemId",
                table: "ItemProperty",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
