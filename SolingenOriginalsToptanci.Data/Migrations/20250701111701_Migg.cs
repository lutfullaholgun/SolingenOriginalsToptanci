using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolingenOriginalsToptanci.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Categories");
        }
    }
}
