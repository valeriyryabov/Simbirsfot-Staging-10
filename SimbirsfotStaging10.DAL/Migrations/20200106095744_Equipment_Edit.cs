using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbirsfotStaging10.DAL.Migrations
{
    public partial class Equipment_Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Equipment",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Equipment",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Equipment");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Equipment",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
