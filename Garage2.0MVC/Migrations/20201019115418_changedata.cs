using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class changedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegNum",
                table: "VehicleModel",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "VehicleModel",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "VehicleModel",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "VehicleModel",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "VehicleModel",
                columns: new[] { "Id", "ArrivalTime", "Brand", "Color", "Model", "NumWheels", "RegNum", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 10, 19, 13, 54, 18, 167, DateTimeKind.Local).AddTicks(1896), "Volvo", "Red", "V70", 4, "ABC123", 0 },
                    { 2, new DateTime(2020, 10, 19, 13, 54, 18, 174, DateTimeKind.Local).AddTicks(3631), "Saab", "Blue", "T20", 6, "GHT253", 1 },
                    { 3, new DateTime(2020, 10, 19, 13, 54, 18, 174, DateTimeKind.Local).AddTicks(3702), "BMW", "Black", "800", 0, "TYU589", 2 },
                    { 4, new DateTime(2020, 10, 19, 13, 54, 18, 174, DateTimeKind.Local).AddTicks(3713), "SAS", "Silver", "737", 6, "SK1420", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "RegNum",
                table: "VehicleModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "VehicleModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "VehicleModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "VehicleModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);
        }
    }
}
