using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace TimeControl.Migrations
{
    public partial class colummLastTimeWorked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastTimeWorked",
                table: "Activity",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "LastTimeWorked", table: "Activity");
        }
    }
}
