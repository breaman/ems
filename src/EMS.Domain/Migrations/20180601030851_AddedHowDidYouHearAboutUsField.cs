using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Domain.Migrations
{
    public partial class AddedHowDidYouHearAboutUsField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HowDidYouHearAboutUs",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HowDidYouHearAboutUs",
                table: "AspNetUsers");
        }
    }
}
