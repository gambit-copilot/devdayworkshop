using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechItEasyBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedRenamedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "NetPrice");

            migrationBuilder.RenameColumn(
                name: "TaxAmountInPreferredCurrency",
                table: "Orders",
                newName: "GrossPriceInPreferredCurrency");

            migrationBuilder.RenameColumn(
                name: "TaxAmount",
                table: "Orders",
                newName: "GrossPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderLine",
                newName: "NetPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NetPrice",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "GrossPriceInPreferredCurrency",
                table: "Orders",
                newName: "TaxAmountInPreferredCurrency");

            migrationBuilder.RenameColumn(
                name: "GrossPrice",
                table: "Orders",
                newName: "TaxAmount");

            migrationBuilder.RenameColumn(
                name: "NetPrice",
                table: "OrderLine",
                newName: "Price");
        }
    }
}
