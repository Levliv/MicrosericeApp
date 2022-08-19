using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientService.EF.Migrations
{
    [Migration("AddDateOfRegistrationToCustomersTable")]
    [DbContext(typeof(ApplicationDbContext))]
    public class AddDateOfRegistrationToCustomersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRegistration",
                table: "Customers", 
                nullable: true
            );
        }
    }
}