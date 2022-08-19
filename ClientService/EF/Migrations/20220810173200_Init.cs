using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ClientService.EF.Migrations
{
    [Migration("Init")]
    [DbContext(typeof(ApplicationDbContext))]
    public class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                    name: "Customers",
                    columns: table => new
                    {
                        Id = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                        Login = table.Column<string>(nullable: false, maxLength: 250),
                        FirstName = table.Column<string>(nullable: false),
                        SecondName = table.Column<string>(nullable: true)
                    }
                )
                .PrimaryKey("PK_Id_Customers", x => x.Id);

            migrationBuilder.AddUniqueConstraint(
                name: "UNIQUE_Customers_Login",
                table: "Customers",
                column: "Login"
            );

            migrationBuilder.CreateTable(
                    name: "Orders",
                    columns: table => new
                    {
                        Id = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                        CustomerId = table.Column<Guid>(nullable: false),
                        OrderTime = table.Column<DateTime>(nullable: false, defaultValue: DateTime.Now),
                        DeliveryTime = table.Column<DateTime>(nullable: true)
                    })
                .PrimaryKey("Pk_Id_Orders", x => x.Id);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CustomerId_To_Customers_Id",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.CreateTable(
                    name: "BakedGoods",
                    columns: table => new
                    {
                        Id = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                        Name = table.Column<string>(nullable: false)
                    })
                .PrimaryKey("PK_BakedGoods", x => x.Id);

            migrationBuilder.CreateTable(
                    name: "BakedGoodsOrders",
                    columns: table => new
                    {
                        Id = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                        OrderId = table.Column<Guid>(nullable: false),
                        BakedGoodId = table.Column<Guid>(nullable: false),
                        ProductWeight = table.Column<float>(nullable: false)
                    })
                .PrimaryKey("PK_BakedGoodsOrders", x => x.Id);

            migrationBuilder.AddForeignKey(
                name: "FK_BakedGoodsOrders_BakedGoodId_To_BakedGoods_Id",
                table: "BakedGoodsOrders",
                column: "BakedGoodId",
                principalTable: "BakedGoods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BakedGoodsOrders_OrderId_To_Orders_Id",
                table: "BakedGoodsOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id"
            );
        }
    }
}