using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FillUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO products.Units (Code, Description)
                                    VALUES 
                                    ('pc', 'piece - the basic unit of quantity'),
                                    ('pair', 'unit of quantity'),
                                    ('set', 'unit of quantity')");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM products.Units");
        }
    }
}
