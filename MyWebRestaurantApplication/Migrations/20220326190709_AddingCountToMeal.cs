using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebRestaurantApplication.Migrations
{
    public partial class AddingCountToMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Meals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Meals");
        }
    }
}
