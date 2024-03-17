using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThePortfo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HTML",
                table: "Template",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "LayoutHTML",
                table: "Template",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LayoutHTML",
                table: "Template");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Template",
                newName: "HTML");
        }
    }
}
