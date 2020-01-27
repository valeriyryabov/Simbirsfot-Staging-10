using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbirsfotStaging10.DAL.Migrations
{
    public partial class Equipment_Types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Equipment",
                newName: "ModelName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Equipment",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentTypeId",
                table: "Equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Equipment",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Equipment",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Size",
                table: "Equipment",
                nullable: true,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    AgeSize = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentTypes_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentTypes_EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Equipment");

            migrationBuilder.RenameColumn(
                name: "ModelName",
                table: "Equipment",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Equipment",
                newName: "Name");
        }
    }
}
