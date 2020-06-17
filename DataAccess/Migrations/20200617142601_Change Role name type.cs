using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ChangeRolenametype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLog_Tasks_TaskId",
                table: "TaskLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLog",
                table: "TaskLog");

            migrationBuilder.RenameTable(
                name: "TaskLog",
                newName: "TaskLogs");

            migrationBuilder.RenameIndex(
                name: "IX_TaskLog_TaskId",
                table: "TaskLogs",
                newName: "IX_TaskLogs_TaskId");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Roles",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLogs",
                table: "TaskLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLogs_Tasks_TaskId",
                table: "TaskLogs",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLogs_Tasks_TaskId",
                table: "TaskLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLogs",
                table: "TaskLogs");

            migrationBuilder.RenameTable(
                name: "TaskLogs",
                newName: "TaskLog");

            migrationBuilder.RenameIndex(
                name: "IX_TaskLogs_TaskId",
                table: "TaskLog",
                newName: "IX_TaskLog_TaskId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLog",
                table: "TaskLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLog_Tasks_TaskId",
                table: "TaskLog",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
