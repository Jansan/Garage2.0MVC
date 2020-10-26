using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 4, 44, 710, DateTimeKind.Local).AddTicks(4117));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 4, 44, 714, DateTimeKind.Local).AddTicks(1945));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 4, 44, 714, DateTimeKind.Local).AddTicks(2013));

            migrationBuilder.UpdateData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArrivalTime",
                value: new DateTime(2020, 10, 19, 14, 4, 44, 714, DateTimeKind.Local).AddTicks(2021));
        }
    }
}
