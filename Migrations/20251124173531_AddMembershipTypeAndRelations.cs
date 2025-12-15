using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipTypeAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MembershipTypeId",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomerMovie",
                columns: table => new
                {
                    CustomersId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMovie", x => new { x.CustomersId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CustomerMovie_customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerMovie_movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "membershipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SignupFee = table.Column<float>(type: "real", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    DiscountRate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membershipTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_MembershipTypeId",
                table: "customers",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMovie_MoviesId",
                table: "CustomerMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_membershipTypes_MembershipTypeId",
                table: "customers",
                column: "MembershipTypeId",
                principalTable: "membershipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_membershipTypes_MembershipTypeId",
                table: "customers");

            migrationBuilder.DropTable(
                name: "CustomerMovie");

            migrationBuilder.DropTable(
                name: "membershipTypes");

            migrationBuilder.DropIndex(
                name: "IX_customers_MembershipTypeId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "MembershipTypeId",
                table: "customers");
        }
    }
}
