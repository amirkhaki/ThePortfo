using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThePortfo.Data.Migrations
{
    /// <inheritdoc />
    public partial class TemplateCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Profile",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HTML = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profile_TemplateId",
                table: "Profile",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Template_TemplateId",
                table: "Profile",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Template_TemplateId",
                table: "Profile");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropIndex(
                name: "IX_Profile_TemplateId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Profile");
        }
    }
}
