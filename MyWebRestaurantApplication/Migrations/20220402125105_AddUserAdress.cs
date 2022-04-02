using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebRestaurantApplication.Migrations
{
    public partial class AddUserAdress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAdress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAdress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "AspNetUsers");
        }
    }
}
