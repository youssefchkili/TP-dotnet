using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoviesStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update stock for existing movies
            migrationBuilder.Sql("UPDATE movies SET Stock = 10 WHERE Id = 3"); // The Shawshank Redemption
            migrationBuilder.Sql("UPDATE movies SET Stock = 5 WHERE Id = 4");  // The Godfather
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE movies SET Stock = 0 WHERE Id = 3");
            migrationBuilder.Sql("UPDATE movies SET Stock = 0 WHERE Id = 4");
        }
    }
}
