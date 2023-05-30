using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakerChecker.Data.Migrations
{
    public partial class addcreateStatusinpostStaging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateStatus",
                table: "PostStagings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateStatus",
                table: "PostStagings");
        }
    }
}
