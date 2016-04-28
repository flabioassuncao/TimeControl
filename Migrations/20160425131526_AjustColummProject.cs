using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace TimeControl.Migrations
{
    public partial class AjustColummProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Activity_Project_ProjectProjectId", table: "Activity");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_User_MemberId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_UsersProjects_ProjectId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_User_AdministratorId", table: "UsersProjects");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_Project_ProjectId", table: "UsersProjects");
            migrationBuilder.DropColumn(name: "ProjectProjectId", table: "Activity");
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Activity",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Project_ProjectId",
                table: "Activity",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_BelongToProject_User_MemberId",
                table: "BelongToProject",
                column: "MemberId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_BelongToProject_UsersProjects_ProjectId",
                table: "BelongToProject",
                column: "ProjectId",
                principalTable: "UsersProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UsersProjects_User_AdministratorId",
                table: "UsersProjects",
                column: "AdministratorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UsersProjects_Project_ProjectId",
                table: "UsersProjects",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Activity_Project_ProjectId", table: "Activity");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_User_MemberId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_UsersProjects_ProjectId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_User_AdministratorId", table: "UsersProjects");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_Project_ProjectId", table: "UsersProjects");
            migrationBuilder.DropColumn(name: "ProjectId", table: "Activity");
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectProjectId",
                table: "Activity",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Project_ProjectProjectId",
                table: "Activity",
                column: "ProjectProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_BelongToProject_User_MemberId",
                table: "BelongToProject",
                column: "MemberId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_BelongToProject_UsersProjects_ProjectId",
                table: "BelongToProject",
                column: "ProjectId",
                principalTable: "UsersProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UsersProjects_User_AdministratorId",
                table: "UsersProjects",
                column: "AdministratorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UsersProjects_Project_ProjectId",
                table: "UsersProjects",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
