using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace TimeControl.Migrations
{
    public partial class ModelsPadraoCamelCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropForeignKey(name: "FK_Time_Activity_activityId", table: "Time");
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Time",
                newName: "Status");
            migrationBuilder.RenameColumn(
                name: "activityId",
                table: "Time",
                newName: "ActivityId");
            migrationBuilder.RenameColumn(
                name: "activityId",
                table: "Activity",
                newName: "ActivityId");
            
            
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropForeignKey(name: "FK_Time_Activity_ActivityId", table: "Time");
            
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Time",
                newName: "status");
            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Time",
                newName: "activityId");
            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Activity",
                newName: "activityId");
            
            
            
        }
    }
}
