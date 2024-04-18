using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManagerBack.Migrations
{
    public partial class AddRegPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRegular",
                table: "Transactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegularPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    WalletId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegularPayments_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegularPayments_WalletId",
                table: "RegularPayments",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegularPayments");

            migrationBuilder.DropColumn(
                name: "IsRegular",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Transactions");
        }
    }
}
