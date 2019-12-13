using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbirsfotStaging10.DAL.Migrations
{
    public partial class _Equipment_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Equipment");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Equipment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Equipment");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Equipment",
                nullable: false,
                defaultValue: 0);
        }
    }
}
