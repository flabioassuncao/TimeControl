using System;
using Microsoft.Data.Entity.Migrations;

namespace TimeControl.Migrations
{
    public partial class Responsibles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Responsible",
                columns: table => new
                {
                    responsibleId = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsible", x => x.responsibleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Responsible");
        }
    }
}
