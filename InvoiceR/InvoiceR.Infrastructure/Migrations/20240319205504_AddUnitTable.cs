using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                schema: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                schema: "products",
                table: "Products",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Units_UnitId",
                schema: "products",
                table: "Products",
                column: "UnitId",
                principalSchema: "products",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Units_UnitId",
                schema: "products",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UnitId",
                schema: "products",
                table: "Products");
        }
    }
}
