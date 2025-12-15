using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStockAndIsSubscribed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribed",
                table: "customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Stock",
                value: 0);

            migrationBuilder.UpdateData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Stock",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "IsSubscribed",
                table: "customers");
        }
    }
}
