using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FillCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO definitions.Currencies (Symbol, Name, IsDefault)
                                    VALUES 
                                    ('PLN', 'Polish zloty', 1),
                                    ('EUR', 'Euro', 0),
                                    ('USD', 'U.S. Dollar', 0)");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM definitions.Currencies");
        }
    }
}
