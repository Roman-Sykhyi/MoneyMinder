using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManagerBack.Migrations
{
    public partial class UpdateCategoryLimitModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLimits_Categories_CategoryId",
                table: "CategoryLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLimits_Wallets_WalletId",
                table: "CategoryLimits");

            migrationBuilder.DropIndex(
                name: "IX_CategoryLimits_CategoryId",
                table: "CategoryLimits");

            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "CategoryLimits",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryLimits",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLimits_Wallets_WalletId",
                table: "CategoryLimits",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLimits_Wallets_WalletId",
                table: "CategoryLimits");

            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "CategoryLimits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryLimits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLimits_CategoryId",
                table: "CategoryLimits",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLimits_Categories_CategoryId",
                table: "CategoryLimits",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLimits_Wallets_WalletId",
                table: "CategoryLimits",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
