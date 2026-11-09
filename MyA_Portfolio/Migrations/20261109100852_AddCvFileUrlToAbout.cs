using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyA_Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddCvFileUrlToAbout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvFileUrl",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvFileUrl",
                table: "Abouts");
        }
    }
}
