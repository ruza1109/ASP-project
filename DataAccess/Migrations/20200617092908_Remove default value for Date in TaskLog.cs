using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RemovedefaultvalueforDateinTaskLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "TaskLog",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 6, 17, 11, 27, 1, 942, DateTimeKind.Local).AddTicks(9708));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "TaskLog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 6, 17, 11, 27, 1, 942, DateTimeKind.Local).AddTicks(9708),
                oldClrType: typeof(DateTime));
        }
    }
}
