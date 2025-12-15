using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAjoutMovie",
                table: "movies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "movies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAjoutMovie",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "movies");
        }
    }
}
