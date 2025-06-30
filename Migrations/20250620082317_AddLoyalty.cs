using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddLoyalty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoyaltyAccounts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(191)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PointsBalance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyAccounts", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_LoyaltyAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoyaltyAccounts");
        }
    }
}
