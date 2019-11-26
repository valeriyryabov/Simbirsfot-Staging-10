using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbirsfotStaging10.DAL.Migrations
{
    public partial class ChangeEventLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventLog_Cards_CardId",
                table: "EventLog");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLog_Platforms_PlatformId",
                table: "EventLog");

            migrationBuilder.AlterColumn<int>(
                name: "PlatformId",
                table: "EventLog",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "EventLog",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "EventLog",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLog_Cards_CardId",
                table: "EventLog",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLog_Platforms_PlatformId",
                table: "EventLog",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventLog_Cards_CardId",
                table: "EventLog");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLog_Platforms_PlatformId",
                table: "EventLog");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "EventLog");

            migrationBuilder.AlterColumn<int>(
                name: "PlatformId",
                table: "EventLog",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "EventLog",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLog_Cards_CardId",
                table: "EventLog",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLog_Platforms_PlatformId",
                table: "EventLog",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
