using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThePortfo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateIdToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Template_TemplateId",
                table: "Profile");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "Profile",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Template_TemplateId",
                table: "Profile",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Template_TemplateId",
                table: "Profile");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "Profile",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Template_TemplateId",
                table: "Profile",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id");
        }
    }
}
