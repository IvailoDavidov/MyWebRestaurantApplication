using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebRestaurantApplication.Migrations
{
    public partial class MealsAddCollectionOfShoppingCarts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_ShoppingCart_ShoppingCartId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_ShoppingCartId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "MealShoppingCart",
                columns: table => new
                {
                    MealsId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartsId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealShoppingCart", x => new { x.MealsId, x.ShoppingCartsId });
                    table.ForeignKey(
                        name: "FK_MealShoppingCart_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealShoppingCart_ShoppingCart_ShoppingCartsId",
                        column: x => x.ShoppingCartsId,
                        principalTable: "ShoppingCart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealShoppingCart_ShoppingCartsId",
                table: "MealShoppingCart",
                column: "ShoppingCartsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealShoppingCart");

            migrationBuilder.AddColumn<string>(
                name: "ShoppingCartId",
                table: "Meals",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_ShoppingCartId",
                table: "Meals",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_ShoppingCart_ShoppingCartId",
                table: "Meals",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
