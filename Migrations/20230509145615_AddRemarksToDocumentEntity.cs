using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    public partial class AddRemarksToDocumentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Document");
        }
    }
}
