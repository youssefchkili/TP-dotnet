using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedMoviesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "Id", "DateAjoutMovie", "GenreId", "ImageFile", "Name" },
                values: new object[,]
                {
                    { 3, new DateTime(2024, 1, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), 3, null, "The Shawshank Redemption" },
                    { 4, new DateTime(2024, 2, 20, 14, 45, 0, 0, DateTimeKind.Unspecified), 1, null, "The Godfather" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "Id", "DateAjoutMovie", "GenreId", "ImageFile", "Name" },
                values: new object[,]
                {
                    { 100, new DateTime(2024, 1, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), 1, null, "The Shawshank Redemption" },
                    { 101, new DateTime(2024, 2, 20, 14, 45, 0, 0, DateTimeKind.Unspecified), 1, null, "The Godfather" }
                });
        }
    }
}
