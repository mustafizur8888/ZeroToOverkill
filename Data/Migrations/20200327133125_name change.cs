using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class namechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupEntities",
                schema: "public",
                table: "GroupEntities");

            migrationBuilder.RenameTable(
                name: "GroupEntities",
                schema: "public",
                newName: "Groups",
                newSchema: "public");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                schema: "public",
                table: "Groups",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                schema: "public",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Groups",
                schema: "public",
                newName: "GroupEntities",
                newSchema: "public");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupEntities",
                schema: "public",
                table: "GroupEntities",
                column: "Id");
        }
    }
}
