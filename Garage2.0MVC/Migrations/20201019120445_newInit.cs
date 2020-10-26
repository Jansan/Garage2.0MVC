using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class newInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    RegNum = table.Column<string>(maxLength: 6, nullable: false),
                    Color = table.Column<string>(maxLength: 20, nullable: false),
                    Brand = table.Column<string>(maxLength: 25, nullable: false),
                    Model = table.Column<string>(maxLength: 25, nullable: false),
                    NumWheels = table.Column<int>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VehicleModel",
                columns: new[] { "Id", "ArrivalTime", "Brand", "Color", "Model", "NumWheels", "RegNum", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 10, 19, 14, 4, 44, 710, DateTimeKind.Local).AddTicks(4117), "Volvo", "Red", "V70", 4, "ABC123", 0 },
                    { 2, new DateTime(2020, 10, 19, 14, 4, 44, 714, DateTimeKind.Local).AddTicks(1945), "Saab", "Blue", "T20", 6, "GHT253", 1 },
                    { 3, new DateTime(2020, 10, 19, 14, 4, 44, 714, DateTimeKind.Local).AddTicks(2013), "BMW", "Black", "800", 0, "TYU589", 2 },
                    { 4, new DateTime(2020, 10, 19, 14, 4, 44, 714, DateTimeKind.Local).AddTicks(2021), "SAS", "Silver", "737", 6, "SK1420", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleModel");
        }
    }
}
