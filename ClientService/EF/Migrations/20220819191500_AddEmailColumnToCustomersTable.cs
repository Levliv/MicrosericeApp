using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientService.EF.Migrations
{
    [Migration("AddEmailColumnToCustomersTable")]
    [DbContext(typeof(ApplicationDbContext))]
    public class AddEmailColumnToCustomersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: true);
        }
    }
}