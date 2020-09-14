using Microsoft.EntityFrameworkCore.Migrations;

namespace CostaRicaApi.Migrations
{
    public partial class ExpenseModelCurrencyRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Expenses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
