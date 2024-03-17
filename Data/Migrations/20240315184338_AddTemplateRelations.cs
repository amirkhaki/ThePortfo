using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThePortfo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Template",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Template_UserId",
                table: "Template",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_AspNetUsers_UserId",
                table: "Template",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_AspNetUsers_UserId",
                table: "Template");

            migrationBuilder.DropIndex(
                name: "IX_Template_UserId",
                table: "Template");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Template");
        }
    }
}
