using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderedFoods",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedFoods", x => new { x.OrderId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_OrderedFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedFoods_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedFoods_FoodId",
                table: "OrderedFoods",
                column: "FoodId");

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Name", "Description", "Category", "Price" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Калифорния Рол з Креветками", "Авокадо, огірок, креветки, омлет", "Суші", 125.00m },
                    { Guid.NewGuid(), "Мисо Суп з Куркою", "Мисо, курка, товстоширокі гриби, цибуля, соус", "Суші", 75.00m },
                    { Guid.NewGuid(), "Імбирне морозиво", "Імбирне морозиво з медом та корицею", "Напої", 35.00m },
                    { Guid.NewGuid(), "Кальцоне зі шпинатом", "Томатний соус, сир Моцарелла, шпинат, часник, шампіньйони", "Піца", 150.00m },
                    { Guid.NewGuid(), "Рол з тунцем", "Тунець, огірок, авокадо, соус Соєвий", "Суші", 100.00m },
                    { Guid.NewGuid(), "Курина грудинка на грилі", "Картопля Фрі, гостра BBQ пастила", "Напої", 85.00m },
                    { Guid.NewGuid(), "Маргарита", "Томатний соус, сир Моцарелла, орегано", "Піца", 100.00m },
                    { Guid.NewGuid(), "Рол з лососем", "Лосось, огірок, авокадо, соус Соєвий", "Суші", 120.00m },
                    { Guid.NewGuid(), "Суші з лососем", "Соковиті шматочки свіжого лосося, зв'язані рисом та огірком", "Суші", 125.00m },
                    { Guid.NewGuid(), "Суші з креветкою", "Креветки, приготовлені на грилі та зв'язані рисом", "Суші", 105.00m },
                    { Guid.NewGuid(), "Суші з вугрем", "Іскристо-білосніжне м'ясо вугра нарізане тоненькими шматочками з рисом та норі", "Суші", 135.00m },
                    { Guid.NewGuid(), "Суші з огірком", "Свіжий огірок та рис", "Суші", 50.00m },
                    { Guid.NewGuid(), "Піца Маргарита", "Томатний соус, моцарела та орегано", "Піца", 90.00m },
                    { Guid.NewGuid(), "Піца з шинкою та грибами", "Томатний соус, моцарела, шинка та гриби", "Піца", 120.00m },
                    { Guid.NewGuid(), "Піца з куркою та броколі", "Томатний соус, моцарела, курка, броколі та часник", "Піца", 130.00m },
                    { Guid.NewGuid(), "Піца з тунцем та оливками", "Томатний соус, моцарела, тунець, оливки та цибуля", "Піца", 140.00m },
                    { Guid.NewGuid(), "Напій Coca-Cola", "Класичний прохолодний напій", "Напої", 30.00m },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedFoods");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
