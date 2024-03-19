using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FillVatRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO definitions.VatRates (Symbol, Value)
                                    VALUES 
                                    ('NP', 0.00),
                                    ('ZW', 0.00),
                                    ('7%', 0.07),
                                    ('0%', 0.00),
                                    ('5%', 0.05),
                                    ('23%', 0.23),
                                    ('8%', 0.08),
                                    ('4%', 0.04),
                                    ('-', 0.00)");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM definitions.VatRates");
        }
    }
}
