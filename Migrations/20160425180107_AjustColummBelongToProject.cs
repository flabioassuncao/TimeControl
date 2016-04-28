using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace TimeControl.Migrations
{
    public partial class AjustColummBelongToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_User_MemberId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_UsersProjects_ProjectId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_User_AdministratorId", table: "UsersProjects");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_Project_ProjectId", table: "UsersProjects");
            migrationBuilder.DropPrimaryKey(name: "PK_BelongToProject", table: "BelongToProject");
            migrationBuilder.DropColumn(name: "Id", table: "BelongToProject");
            migrationBuilder.AddColumn<Guid>(
                name: "BelongToProjectMemberId",
                table: "UsersProjects",
                nullable: true);
            migrationBuilder.AddColumn<Guid>(
                name: "BelongToProjectProjectId",
                table: "UsersProjects",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                nullable: false);
            migrationBuilder.AddColumn<Guid>(
                name: "AdministratorId",
                table: "Project",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
            migrationBuilder.AddPrimaryKey(
                name: "PK_BelongToProject",
                table: "BelongToProject",
                columns: new[] { "ProjectId", "MemberId" });
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_BelongToProject_User_MemberId",
                table: "BelongToProject",
                column: "MemberId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_BelongToProject_Project_ProjectId",
                table: "BelongToProject",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_AdministratorId",
                table: "Project",
                column: "AdministratorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
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
            migrationBuilder.AddForeignKey(
                name: "FK_UsersProjects_BelongToProject_BelongToProjectProjectId_BelongToProjectMemberId",
                table: "UsersProjects",
                columns: new[] { "BelongToProjectProjectId", "BelongToProjectMemberId" },
                principalTable: "BelongToProject",
                principalColumns: new[] { "ProjectId", "MemberId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_User_MemberId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_BelongToProject_Project_ProjectId", table: "BelongToProject");
            migrationBuilder.DropForeignKey(name: "FK_Project_User_AdministratorId", table: "Project");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_User_AdministratorId", table: "UsersProjects");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_Project_ProjectId", table: "UsersProjects");
            migrationBuilder.DropForeignKey(name: "FK_UsersProjects_BelongToProject_BelongToProjectProjectId_BelongToProjectMemberId", table: "UsersProjects");
            migrationBuilder.DropPrimaryKey(name: "PK_BelongToProject", table: "BelongToProject");
            migrationBuilder.DropColumn(name: "BelongToProjectMemberId", table: "UsersProjects");
            migrationBuilder.DropColumn(name: "BelongToProjectProjectId", table: "UsersProjects");
            migrationBuilder.DropColumn(name: "AdministratorId", table: "Project");
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                nullable: true);
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BelongToProject",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
            migrationBuilder.AddPrimaryKey(
                name: "PK_BelongToProject",
                table: "BelongToProject",
                column: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
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
