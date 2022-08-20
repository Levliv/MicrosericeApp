using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientService.EF.Migrations
{
    [Migration("AddIsActiveColumnToCustomersTable")]
    [DbContext(typeof(ApplicationDbContext))]
    public class AddIsActiveColumnToCustomersTable : Migration 
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Customers",
                nullable: false,
                defaultValue: true
            );
        }
    }
}