using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class Garage3Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleModel",
                columns: new[] { "Id", "ArrivalTime", "Brand", "Color", "MemberId", "Model", "NumWheels", "RegNum", "Type", "VehicleNum", "VehicleTypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 10, 26, 16, 50, 6, 803, DateTimeKind.Local).AddTicks(2325), "Volvo", "Red", 0, "V70", 4, "ABC123", 0, null, 0 },
                    { 2, new DateTime(2020, 10, 26, 16, 50, 6, 811, DateTimeKind.Local).AddTicks(5826), "Saab", "Blue", 0, "T20", 6, "GHT253", 1, null, 0 },
                    { 3, new DateTime(2020, 10, 26, 16, 50, 6, 811, DateTimeKind.Local).AddTicks(5926), "BMW", "Black", 0, "800", 0, "TYU589", 2, null, 0 },
                    { 4, new DateTime(2020, 10, 26, 16, 50, 6, 811, DateTimeKind.Local).AddTicks(5944), "SAS", "Silver", 0, "737", 6, "SK1420", 3, null, 0 }
                });
        }
    }
}
