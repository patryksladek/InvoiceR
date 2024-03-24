using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FillExchangeRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO definitions.ExchangeRates (CurrencyId, Date, Rate, TableNumber)
                                    VALUES 
                                    (2, '2000-01-01 00:00:00.000', 4.30, ''),
                                    (3, '2000-01-01 00:00:00.000', 4.00, '')");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM definitions.ExchangeRates");
        }
    }
}
