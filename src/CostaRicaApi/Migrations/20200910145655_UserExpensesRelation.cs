using Microsoft.EntityFrameworkCore.Migrations;

namespace CostaRicaApi.Migrations
{
    public partial class UserExpensesRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Expenses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_OwnerId",
                table: "Expenses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_OwnerId",
                table: "Expenses",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_OwnerId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_OwnerId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Expenses");
        }
    }
}
