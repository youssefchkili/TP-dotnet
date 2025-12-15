using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewMoviesFromJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Stock",
                value: 10);

            migrationBuilder.UpdateData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Stock",
                value: 5);

            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "Id", "DateAjoutMovie", "GenreId", "ImageFile", "Name", "Stock" },
                values: new object[,]
                {
                    { 5, new DateTime(2024, 3, 10, 16, 20, 0, 0, DateTimeKind.Unspecified), 1, null, "Mad Max: Fury Road", 8 },
                    { 6, new DateTime(2024, 4, 5, 12, 15, 0, 0, DateTimeKind.Unspecified), 1, null, "Die Hard", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 6);

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
    }
}
