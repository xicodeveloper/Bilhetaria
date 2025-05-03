using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wallet_users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MbwaySaldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 100m),
                    ApplePaySaldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 100m),
                    CreditCardSaldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 100m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallet_users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_wallet_users_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wallet_users");
        }
    }
}
