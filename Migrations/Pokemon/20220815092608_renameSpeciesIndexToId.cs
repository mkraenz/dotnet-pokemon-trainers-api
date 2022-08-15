using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnettest.Migrations
{
    public partial class renameSpeciesIndexToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Index",
                table: "Species",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Species",
                newName: "Index");
        }
    }
}
