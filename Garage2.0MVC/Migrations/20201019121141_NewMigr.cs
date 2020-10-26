using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class NewMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 11, 40, 371, DateTimeKind.Local).AddTicks(4207));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 11, 40, 380, DateTimeKind.Local).AddTicks(9796));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 11, 40, 380, DateTimeKind.Local).AddTicks(9935));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 11, 40, 380, DateTimeKind.Local).AddTicks(9947));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 9, 36, 752, DateTimeKind.Local).AddTicks(1746));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 9, 36, 758, DateTimeKind.Local).AddTicks(2765));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 9, 36, 758, DateTimeKind.Local).AddTicks(2871));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 9, 36, 758, DateTimeKind.Local).AddTicks(2882));
        }
    }
}
