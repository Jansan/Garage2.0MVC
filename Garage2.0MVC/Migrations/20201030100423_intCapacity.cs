using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2._0MVC.Migrations
{
    public partial class intCapacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "VehicleType",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Capacity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Capacity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Capacity",
                value: 2);

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Capacity",
                value: 2);

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Capacity",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Capacity",
                table: "VehicleType",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Capacity",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Capacity",
                value: 0.29999999999999999);

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Capacity",
                value: 2.0);

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Capacity",
                value: 2.0);

            migrationBuilder.UpdateData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Capacity",
                value: 3.0);
        }
    }
}
