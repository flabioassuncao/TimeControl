using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace TimeControl.Migrations
{
    public partial class ColummUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_ApplicationUser_MemberId1", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_UsersProjects_ProjectId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_ApplicationUser_AdministratorId1", table: "UsersProjects");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_Project_ProjectId", table: "UsersProjects");
            migrationBuilder.DropColumn(name: "AdministratorId1", table: "UsersProjects");
            migrationBuilder.DropColumn(name: "MemberId1", table: "BelongToProject");
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
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
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_User_MemberId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_UsersProjects_ProjectId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_User_AdministratorId", table: "UsersProjects");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_Project_ProjectId", table: "UsersProjects");
            migrationBuilder.DropTable("User");
            migrationBuilder.AddColumn<string>(
                name: "AdministratorId1",
                table: "UsersProjects",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "MemberId1",
                table: "BelongToProject",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_BelongToProject_ApplicationUser_MemberId1",
                table: "BelongToProject",
                column: "MemberId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_BelongToProject_UsersProjects_ProjectId",
                table: "BelongToProject",
                column: "ProjectId",
                principalTable: "UsersProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UsersProjects_ApplicationUser_AdministratorId1",
                table: "UsersProjects",
                column: "AdministratorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
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
