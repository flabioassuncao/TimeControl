using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace TimeControl.Migrations
{
    public partial class TableProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    ProjectName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                });
            migrationBuilder.CreateTable(
                name: "UsersProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdministratorId = table.Column<Guid>(nullable: false),
                    AdministratorId1 = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersProjects_ApplicationUser_AdministratorId1",
                        column: x => x.AdministratorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersProjects_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "BelongToProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    MemberId1 = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BelongToProject_ApplicationUser_MemberId1",
                        column: x => x.MemberId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BelongToProject_UsersProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "UsersProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Activity_Project_ProjectProjectId", table: "Activity");
            migrationBuilder.DropColumn(name: "ProjectProjectId", table: "Activity");
            migrationBuilder.DropTable("BelongToProject");
            migrationBuilder.DropTable("UsersProjects");
            migrationBuilder.DropTable("Project");
            
        }
    }
}
