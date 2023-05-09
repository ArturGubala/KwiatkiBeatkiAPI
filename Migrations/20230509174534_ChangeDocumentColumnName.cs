using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class ChangeDocumentColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Document",
                newName: "Updated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Document",
                newName: "LastUpdated");
        }
    }
}
